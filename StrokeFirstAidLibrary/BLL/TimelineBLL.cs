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
        public APIResult Check(PatientTimeline patientTimeline)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Timeline selectedTimeLine = freeBaseSQL.GetRepository<Timeline>().Where(a => a.ID == patientTimeline.TimelineID).First();
                if (selectedTimeLine == null)
                {
                    throw new ExceptionUtil("未找到该时间点");
                }

                PatientTimeline selectedPatientTimeline = freeSQL.GetRepository<PatientTimeline>().Where(a => a.PatientID == patientTimeline.PatientID && a.TimelineID == selectedTimeLine.ID).First();
                if (selectedPatientTimeline == null)
                {
                    //patientTimeline.DoctorID=
                    freeSQL.GetRepository<PatientTimeline>().Insert(patientTimeline);
                }
                else
                {
                    freeSQL.GetRepository<PatientTimeline>().Attach(selectedPatientTimeline);
                    selectedPatientTimeline.Remark = patientTimeline.Remark;
                    freeSQL.GetRepository<PatientTimeline>().Update(selectedPatientTimeline);
                }

                result = APIResult.Success("时间点选择成功", selectedTimeLine);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            if (!string.IsNullOrEmpty(message))
            {
                new LogUtil().Add(LogCode.添加错误, message, loginUser);
                result = APIResult.Error(message);
            }

            return result;
        }


        public APIResult CheckedList(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {

                List<int> patientTimeLineList = freeSQL.Select<PatientTimeline>().Where(b => b.PatientID == patientID).ToList(b => b.TimelineID);
                List<Timeline> timelineList = freeBaseSQL.Select<Timeline>().Where(a => patientTimeLineList.Contains(a.ID)).ToList();

                result = APIResult.Success("查询成功", timelineList);
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

        public APIResult NotCheckedList(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                List<int> patientTimeLineList = freeSQL.Select<PatientTimeline>().Where(b => b.PatientID == patientID).ToList(b => b.TimelineID);
                List<Timeline> timelineList = freeBaseSQL.Select<Timeline>().Where(a => !patientTimeLineList.Contains(a.ID)).ToList();

                result = APIResult.Success("查询成功", timelineList);
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
    }
}
