using StrokeFirstAidLibrary.Entity;
using StrokeFirstAidLibrary.Util;
using static StrokeFirstAidLibrary.StrokeFirstAidDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StrokeFirstAidLibrary.BLL
{
    public class ChoiceQuestionBLL
    {
        public APIResult List(int parentID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                ChoiceQuestion choiceQuestion = freeBaseSQL.GetRepository<ChoiceQuestion>().Where(a => a.ID == parentID).First();

                if (choiceQuestion == null)
                {
                    throw new ExceptionUtil("未找到父级");
                }

                ChoiceQuestionGrade choiceQuestionGrade = freeBaseSQL.GetRepository<ChoiceQuestionGrade>().Where(a => a.ID == choiceQuestion.ID).First();

                List<ChoiceQuestion> choiceQuestionList = freeBaseSQL.Select<ChoiceQuestion>().Where(a => a.Layer == choiceQuestion.Layer + 1 && a.Left > choiceQuestion.Left && a.Right < choiceQuestion.Right).OrderBy(t => t.Left).ToList();

                result = APIResult.Success("查询成功", choiceQuestionList);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            if (!string.IsNullOrEmpty(message))
            {
                new LogUtil().Add(LogCode.获取错误, message, loginUser);
                result = APIResult.Error(message);
            }
            return result;
        }

        public APIResult TreeList(int parentID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                ChoiceQuestion choiceQuestion = freeBaseSQL.GetRepository<ChoiceQuestion>().Where(a => a.ID == parentID).First();

                if (choiceQuestion == null)
                {
                    throw new ExceptionUtil("未找到父级");
                }

                List<ChoiceQuestion> fullChoiceQuestionList = freeBaseSQL.Select<ChoiceQuestion>().Where(a => a.Layer > choiceQuestion.Layer && a.Left > choiceQuestion.Left && a.Right < choiceQuestion.Right).OrderBy(t => t.Left).ToList();

                result = APIResult.Success("查询成功", GetChildChoiceQuestionList(choiceQuestion, fullChoiceQuestionList));
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            if (!string.IsNullOrEmpty(message))
            {
                new LogUtil().Add(LogCode.获取错误, message, loginUser);
                result = APIResult.Error(message);
            }
            return result;
        }

        public APIResult Score(int patientID, int choiceQuestionID, List<ChoiceQuestion> choiceQuestionList)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {

                ChoiceQuestion choiceQuestion = freeBaseSQL.GetRepository<ChoiceQuestion>().Where(a => a.ID == choiceQuestionID && a.Layer == 2).First();

                if (choiceQuestion == null)
                {
                    throw new ExceptionUtil("未找到该评分");
                }

                List<ChoiceQuestion> fullChoiceQuestionList = freeBaseSQL.GetRepository<ChoiceQuestion>().Where(a => a.Left > choiceQuestion.Left && a.Right < choiceQuestion.Right && a.Value.HasValue).ToList();
                List<ChoiceQuestionGrade> choiceQuestionGradeList = freeBaseSQL.Select<ChoiceQuestionGrade>().Where(a => a.ChoiceQuestionID == choiceQuestionID).ToList();
                //累计分和不累计分独立计算
                //替换原有PatientChoice
                List<int> fullChoiceQuestionIDList = fullChoiceQuestionList.Select(t => t.ID).ToList();
                freeSQL.Delete<PatientChoice>().Where(a => a.PatientID == patientID && fullChoiceQuestionIDList.Contains(a.ChoiceQuestionID)).ExecuteAffrows();
                if (choiceQuestionGradeList.Count == 0)
                {
                    if (choiceQuestionList != null && choiceQuestionList.Count > 0)
                    {
                        //直接记录选项
                        PatientChoice patientChoice = new PatientChoice();
                        patientChoice.PatientID = patientID;
                        patientChoice.ChoiceQuestionID = choiceQuestionList.First().ID;
                        freeSQL.GetRepository<PatientChoice>().Insert(patientChoice);
                        int score = fullChoiceQuestionList.First(a => a.ID == choiceQuestionList.First().ID).Value.Value;
                        new QuestionRankUtil().Rank((QuestionRankID)choiceQuestionID, patientID, score == 9 ? 0 : score);
                    }
                    else
                    {
                        new QuestionRankUtil().Rank((QuestionRankID)choiceQuestionID, patientID, null);
                    }
                }
                else
                {
                    if (choiceQuestionList != null && choiceQuestionList.Count > 0)
                    {
                        int totalScore = 0;
                        //记录选项并累计积分
                        foreach (ChoiceQuestion selectedChoiceQuestion in choiceQuestionList)
                        {
                            PatientChoice patientChoice = new PatientChoice();
                            patientChoice.PatientID = patientID;
                            patientChoice.ChoiceQuestionID = selectedChoiceQuestion.ID;
                            freeSQL.GetRepository<PatientChoice>().Insert(patientChoice);
                            int score = fullChoiceQuestionList.First(a => a.ID == selectedChoiceQuestion.ID).Value.Value;
                            totalScore += score == 9 ? 0 : score;
                        }
                        new QuestionRankUtil().Rank((QuestionRankID)choiceQuestionID, patientID, totalScore);
                    }
                    else
                    {
                        new QuestionRankUtil().Rank((QuestionRankID)choiceQuestionID, patientID, null);
                    }
                }

                new LogUtil().Add(LogCode.修改, message, loginUser);
                result = APIResult.Success("修改分数成功", null);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            if (!string.IsNullOrEmpty(message))
            {
                new LogUtil().Add(LogCode.修改错误, message, loginUser);
                result = APIResult.Error(message);
            }
            return result;
        }



        public List<ChoiceQuestion> GetChildChoiceQuestionList(ChoiceQuestion choiceQuestion, List<ChoiceQuestion> fullChoiceQuestionList)
        {
            if (choiceQuestion.Value.HasValue)
            {
                return new List<ChoiceQuestion>();
            }
            else
            {
                List<ChoiceQuestion> choiceQuestionList = fullChoiceQuestionList.Where(a => a.Layer == choiceQuestion.Layer + 1 && a.Left > choiceQuestion.Left && a.Right < choiceQuestion.Right).OrderBy(t => t.Left).ToList();
                foreach (ChoiceQuestion selectedChoiceQuestion in choiceQuestionList)
                {
                    selectedChoiceQuestion.ChildChoiceQuestionList = GetChildChoiceQuestionList(selectedChoiceQuestion, fullChoiceQuestionList);
                }

                return choiceQuestionList;
            }
        }
    }
}
