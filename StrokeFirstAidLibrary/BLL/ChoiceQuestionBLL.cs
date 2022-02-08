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
