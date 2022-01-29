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
            try
            {               
                ChoiceQuestion choiceQuestion = freeSQL.Select<ChoiceQuestion>().Where(a => a.ID == parentID).ToOne();

                if (choiceQuestion == null)
                {
                    throw new ExceptionUtil("未找到父级");
                }

                List<ChoiceQuestion> choiceQuestionList = freeSQL.Select<ChoiceQuestion>().Where(a => a.Layer == choiceQuestion.Layer + 1 && a.Left > choiceQuestion.Left && a.Right < choiceQuestion.Right).OrderBy(t => t.Left).ToList();

                result = APIResult.Success("查询成功", choiceQuestionList);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            if (!string.IsNullOrEmpty(message))
            {
                result = APIResult.Error(message);
            }

            return result;
        }
    }
}
