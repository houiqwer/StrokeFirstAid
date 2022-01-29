using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrokeFirstAidLibrary.Entity;
using StrokeFirstAidLibrary.BLL;
using StrokeFirstAidLibrary.Util;

namespace StrokeFirstAidAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        [HttpPost]
        public APIResult Add([FromBody] Patient patient, ArrivalWay arrivalWay)
        {
            return new PatientBLL().Add(patient, arrivalWay);
        }
    }
}
