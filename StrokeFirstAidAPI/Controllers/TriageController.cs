using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrokeFirstAidLibrary.Entity;
using StrokeFirstAidLibrary.BLL;
using StrokeFirstAidLibrary.Util;

namespace StrokeFirstAidAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TriageController : ControllerBase
    {
        [HttpGet, Route("{patientID?}")]
        public APIResult TriageGet(int patientID)
        {
            return new TriageBLL().TriageGet(patientID);
        }

        [HttpPost]
        public APIResult PatientInfoEdit([FromBody] PatientInfo patientInfo)
        {
            return new TriageBLL().PatientInfoEdit(patientInfo);
        }

        [HttpGet, Route("{patientID?}")]
        public APIResult PatientInfoGet(int patientID)
        {
            return new TriageBLL().PatientInfoGet(patientID);
        }

        [HttpPost]
        public APIResult DiseaseTimeEdit([FromBody] Triage triage)
        {
            return new TriageBLL().DiseaseTimeEdit(triage);
        }

        [HttpGet, Route("{patientID?}")]
        public APIResult DiseaseTimeGet(int patientID)
        {
            return new TriageBLL().DiseaseTimeGet(patientID);
        }



        [HttpGet, Route("{patientID?}")]
        public APIResult FASTEDRankGet(int patientID)
        {
            return new TriageBLL().FASTEDRankGet(patientID);
        }
    }
}
