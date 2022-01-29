using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrokeFirstAidLibrary.Entity;
using StrokeFirstAidLibrary.BLL;
using StrokeFirstAidLibrary.Util;

namespace StrokeFirstAidAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TimelineController : ControllerBase
    {
        [HttpGet, Route("{patientID?}")]
        public APIResult CheckedList(int patientID)
        {
            return new TimelineBLL().CheckedList(patientID);
        }

        [HttpGet, Route("{patientID?}")]
        public APIResult NotCheckedList(int patientID)
        {
            return new TimelineBLL().NotCheckedList(patientID);
        }
    }
}
