using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrokeFirstAidLibrary.Entity;
using StrokeFirstAidLibrary.BLL;
using StrokeFirstAidLibrary.Util;

namespace StrokeFirstAidAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ChoiceQuestionController : ControllerBase
    {
        [HttpGet, Route("{parentID?}")]
        public APIResult List(int parentID)
        {
            return new ChoiceQuestionBLL().List(parentID);
        }

        [HttpGet, Route("{parentID?}")]
        public APIResult TreeList(int parentID)
        {
            return new ChoiceQuestionBLL().TreeList(parentID);
        }

        [HttpPost]
        public APIResult Score(int patientID, int choiceQuestionID, List<ChoiceQuestion> choiceQuestionList)
        {
            return new ChoiceQuestionBLL().Score(patientID, choiceQuestionID, choiceQuestionList);
        }
    }
}
