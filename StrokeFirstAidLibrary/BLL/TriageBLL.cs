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
    public class TriageBLL
    {
        public APIResult PatientInfoEdit(PatientInfo patientInfo)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == patientInfo.PatientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }

                PatientInfo selectedPatientInfo = freeSQL.GetRepository<PatientInfo>().Where(a => a.PatientID == patient.ID).First();
                if (selectedPatientInfo == null)
                {
                    freeSQL.GetRepository<PatientInfo>().Insert(selectedPatientInfo);
                }
                else
                {
                    freeSQL.GetRepository<PatientInfo>().Attach(selectedPatientInfo);
                    selectedPatientInfo = BaseUtil.XmlDeepCopy(patientInfo);
                    freeSQL.GetRepository<PatientInfo>().Update(selectedPatientInfo);
                }

                //修改首页显示内容，是否已填写所有数据              
                Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                triage.PatientInfo = selectedPatientInfo.GetFillingStatus();
                freeSQL.GetRepository<Triage>().Update(triage);

                new LogUtil().Add(LogCode.修改, "患者:" + patient.ID + "基本信息成功修改", loginUser);
                result = APIResult.Success("患者基本信息修改成功", selectedPatientInfo);
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
        public APIResult PatientInfoGet(int parentID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == parentID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }
                PatientInfo selectedPatientInfo = freeSQL.GetRepository<PatientInfo>().Where(a => a.PatientID == patient.ID).First();
                result = APIResult.Success("患者基本信息获取成功", selectedPatientInfo);
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

        public APIResult DiseaseTimeEdit(Triage triage)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == triage.PatientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }

                Triage selectedTriage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                if (triage.IsWUS == null)
                {
                    throw new ExceptionUtil("请选择是否为醒后卒中");
                }

                if (triage.IsWUS.Value)
                {
                    selectedTriage.LastNormalTime = triage.LastNormalTime;
                    selectedTriage.FindTime = triage.FindTime;
                }
                else
                {
                    selectedTriage.DiseaseTime = triage.DiseaseTime;
                }
                freeSQL.GetRepository<Triage>().Update(selectedTriage);

                new LogUtil().Add(LogCode.修改, "患者:" + patient.ID + "发病时间成功修改", loginUser);
                result = APIResult.Success("患者发病时间修改成功", triage);
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
        public APIResult DiseaseTimeGet(int parentID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == parentID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }
                Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                result = APIResult.Success("患者发病时间获取成功", new
                {
                    triage.ID,
                    triage.IsWUS,
                    triage.DiseaseTime,
                    triage.LastNormalTime,
                    triage.FindTime
                });
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

        public APIResult EmergencyTreatmentTimeEdit(Triage triage)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == triage.PatientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }

                Triage selectedTriage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                selectedTriage.EmergencyTreatmentTime = triage.EmergencyTreatmentTime;
                freeSQL.GetRepository<Triage>().Update(selectedTriage);

                new LogUtil().Add(LogCode.修改, "患者:" + patient.ID + "急诊分诊时间成功修改", loginUser);
                result = APIResult.Success("患者急诊分诊时间修改成功", triage);
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
        public APIResult EmergencyTreatmentTimeGet(int parentID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == parentID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }
                Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                result = APIResult.Success("患者急诊分诊时间获取成功", new
                {
                    triage.ID,
                    triage.EmergencyTreatmentTime
                });
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

        public APIResult ArrivalTimeEdit(Triage triage)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == triage.PatientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }

                Triage selectedTriage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                selectedTriage.ArrivalTime = triage.ArrivalTime;
                freeSQL.GetRepository<Triage>().Update(selectedTriage);

                new LogUtil().Add(LogCode.修改, "患者:" + patient.ID + "到院时间成功修改", loginUser);
                result = APIResult.Success("患者到院时间修改成功", triage);
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
        public APIResult ArrivalTimeGet(int parentID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == parentID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }
                Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                result = APIResult.Success("患者到院时间获取成功", new
                {
                    triage.ID,
                    triage.ArrivalTime
                });
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

        public APIResult ArrivalWayEdit(Triage triage)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == triage.PatientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }

                Triage selectedTriage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                selectedTriage.ArrivalWay = triage.ArrivalWay;
                freeSQL.GetRepository<Triage>().Update(selectedTriage);

                new LogUtil().Add(LogCode.修改, "患者:" + patient.ID + "到院方式成功修改", loginUser);
                result = APIResult.Success("患者到院方式修改成功", triage);
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
        public APIResult ArrivalWayGet(int parentID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == parentID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }
                Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                result = APIResult.Success("患者到院方式获取成功", new
                {
                    triage.ID,
                    triage.ArrivalWay,
                    ArrivalWayList = BaseUtil.EnumToList<ArrivalWay>()
                });
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

        public APIResult EmergencyReceivingTimeEdit(Triage triage)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == triage.PatientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }

                Triage selectedTriage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                selectedTriage.EmergencyReceivingTime = triage.EmergencyReceivingTime;
                freeSQL.GetRepository<Triage>().Update(selectedTriage);

                new LogUtil().Add(LogCode.修改, "患者:" + patient.ID + "急诊医生接诊时间成功修改", loginUser);
                result = APIResult.Success("患者急诊医生接诊时间修改成功", triage);
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
        public APIResult EmergencyReceivingTimeGet(int parentID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == parentID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }
                Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                result = APIResult.Success("患者急诊医生接诊时间获取成功", new
                {
                    triage.ID,
                    triage.EmergencyReceivingTime
                });
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

        public APIResult CSReceivingTimeEdit(Triage triage)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == triage.PatientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }

                Triage selectedTriage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                selectedTriage.CSReceivingTime = triage.CSReceivingTime;
                selectedTriage.CSReceivingDoctorID = triage.CSReceivingDoctorID;
                freeSQL.GetRepository<Triage>().Update(selectedTriage);

                new LogUtil().Add(LogCode.修改, "患者:" + patient.ID + "卒中医生接诊时间成功修改", loginUser);
                result = APIResult.Success("患者卒中医生接诊时间修改成功", triage);
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
        public APIResult CSReceivingTimeGet(int parentID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == parentID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }
                Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                result = APIResult.Success("患者卒中医生接诊时间获取成功", new
                {
                    triage.ID,
                    triage.CSReceivingTime,
                    triage.CSReceivingDoctorID,
                    //获取同医院doctor
                    DoctorList = freeBaseSQL.Select<Doctor>().Where(a => a.IsDelete != true).ToList()
                });
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

        //public APIResult FASTEDRankEdit(Triage triage)
        //{
        //    APIResult result = new APIResult();
        //    string message = string.Empty;
        //    User loginUser = LoginHelper.CurrentUser();
        //    try
        //    {
        //        Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == triage.PatientID).First();
        //        if (patient == null)
        //        {
        //            throw new ExceptionUtil("未找到该患者");
        //        }

        //        Triage selectedTriage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
        //        selectedTriage.CSReceivingTime = triage.CSReceivingTime;
        //        selectedTriage.CSReceivingDoctorID = triage.CSReceivingDoctorID;
        //        freeSQL.GetRepository<Triage>().Update(selectedTriage);

        //        new LogUtil().Add(LogCode.修改, "患者:" + patient.ID + "卒中医生接诊时间成功修改", loginUser);
        //        result = APIResult.Success("患者卒中医生接诊时间修改成功", triage);
        //    }
        //    catch (Exception ex)
        //    {
        //        message = ex.Message;
        //    }

        //    if (!string.IsNullOrEmpty(message))
        //    {
        //        new LogUtil().Add(LogCode.修改错误, message, loginUser);
        //        result = APIResult.Error(message);
        //    }

        //    return result;
        //}
        
    }
}
