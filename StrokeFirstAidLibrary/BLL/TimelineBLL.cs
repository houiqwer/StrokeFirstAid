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
    public class TimelineBLL
    {
        public APIResult CheckedList(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            try
            {
                List<Timeline> timelineList = freeSQL.Select<Timeline>().Where(a => freeSQL.Select<PatientTimeline>().Where(b => b.PatientID == patientID && b.TimelineID == a.ID).Any()).ToList();

                result = APIResult.Success("查询成功", timelineList);
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

        public APIResult NotCheckedList(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            try
            {
                List<Timeline> timelineList = freeSQL.Select<Timeline>().Where(a =>
                !freeSQL.Select<PatientTimeline>().Where(b => b.PatientID == patientID && a.ID == b.TimelineID).Any()
                ).ToList();

                result = APIResult.Success("查询成功", timelineList);
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
