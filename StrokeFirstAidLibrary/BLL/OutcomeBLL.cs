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
    public class OutcomeBLL
    {
        public APIResult OutcomeGet(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == patientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }
                Outcome outcome = freeSQL.GetRepository<Outcome>().Where(a => a.PatientID == patient.ID).First();
                result = APIResult.Success("患者转归信息获取成功", new
                {
                    outcome.ID,
                    OutcomeDelay = Enum.GetName(typeof(FillingStatus), outcome.OutcomeDelay),
                    PatientGo = outcome.PatientGo.HasValue ? Enum.GetName(typeof(PatientGo), outcome.PatientGo) : FillingStatus.未填写.ToString(),
                    IsFiling = outcome.IsFiling.HasValue ? FillingStatus.已完成.ToString() : "未提交",
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

        public APIResult OutcomeDelayEdit(OutcomeDelay outcomeDelay)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == outcomeDelay.PatientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }

                OutcomeDelay selectedOutcomeDelay = freeSQL.GetRepository<OutcomeDelay>().Where(a => a.PatientID == patient.ID).First();
                if (selectedOutcomeDelay == null)
                {
                    freeSQL.GetRepository<OutcomeDelay>().Insert(selectedOutcomeDelay);
                }
                else
                {
                    freeSQL.GetRepository<OutcomeDelay>().Attach(selectedOutcomeDelay);
                    selectedOutcomeDelay = BaseUtil.XmlDeepCopy(outcomeDelay);
                    freeSQL.GetRepository<OutcomeDelay>().Update(selectedOutcomeDelay);
                }

                //修改首页显示内容，是否已填写所有数据              
                Outcome outcome = freeSQL.GetRepository<Outcome>().Where(a => a.PatientID == patient.ID).First();
                outcome.OutcomeDelay = outcomeDelay.GetFillingStatus();
                freeSQL.GetRepository<Outcome>().Update(outcome);

                new LogUtil().Add(LogCode.修改, "患者:" + patient.ID + "耗时原因成功修改", loginUser);
                result = APIResult.Success("患者耗时原因修改成功", selectedOutcomeDelay);
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
        public APIResult OutcomeDelayGet(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == patientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }

                OutcomeDelay selectedOutcomeDelay = freeSQL.GetRepository<OutcomeDelay>().Where(a => a.PatientID == patient.ID).First();
                result = APIResult.Success("患者耗时原因获取成功", selectedOutcomeDelay);
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
        public APIResult DiseaseTimeGet(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == patientID).First();
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
        public APIResult EmergencyTreatmentTimeGet(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == patientID).First();
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
        public APIResult ArrivalTimeGet(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == patientID).First();
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
        public APIResult ArrivalWayGet(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == patientID).First();
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
        public APIResult EmergencyReceivingTimeGet(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == patientID).First();
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
        public APIResult CSReceivingTimeGet(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == patientID).First();
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

        public APIResult FASTEDRankGet(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == patientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }
                Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                ChoiceQuestion choiceQuestion = freeBaseSQL.GetRepository<ChoiceQuestion>().Where(a => a.ID == (int)QuestionRankID.FASTEDRank && a.Layer == 2).First();

                //获取患者选择的内容
                List<ChoiceQuestion> fullChoiceQuestionList = freeBaseSQL.GetRepository<ChoiceQuestion>().Where(a => a.Left > choiceQuestion.Left && a.Right < choiceQuestion.Right && a.Value.HasValue).ToList();
                List<int> fullChoiceQuestionIDList = fullChoiceQuestionList.Select(t => t.ID).ToList();
                List<PatientChoice> patientChoiceList = freeSQL.Select<PatientChoice>().Where(a => a.PatientID == patientID && fullChoiceQuestionIDList.Contains(a.ChoiceQuestionID)).ToList();

                //获取分级
                List<ChoiceQuestionGrade> choiceQuestionGradeList = freeBaseSQL.GetRepository<ChoiceQuestionGrade>().Where(a => a.ChoiceQuestionID == (int)QuestionRankID.FASTEDRank).ToList();
                List<ChoiceQuestion> fullTreeChoiceQuestionList = freeBaseSQL.Select<ChoiceQuestion>().Where(a => a.Layer > choiceQuestion.Layer && a.Left > choiceQuestion.Left && a.Right < choiceQuestion.Right).OrderBy(t => t.Left).ToList();

                result = APIResult.Success("患者FAST-ED评分获取成功", new
                {
                    triage.ID,
                    triage.FASTEDRank,
                    choiceQuestionGrade = choiceQuestionGradeList.First(t => t.Max >= triage.FASTEDRank),
                    patientChoiceList,
                    choiceQuestionGradeList,
                    TreeChoiceQuestionList = new ChoiceQuestionBLL().GetChildChoiceQuestionList(choiceQuestion, fullTreeChoiceQuestionList),
                    FASTEDRankParentID = (int)QuestionRankID.FASTEDRank,
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

        public APIResult PatientConditionGet(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == patientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }
                Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                ChoiceQuestion choiceQuestion = freeBaseSQL.GetRepository<ChoiceQuestion>().Where(a => a.ID == (int)QuestionRankID.PatientCondition && a.Layer == 2).First();

                //获取患者选择的内容
                List<ChoiceQuestion> fullChoiceQuestionList = freeBaseSQL.GetRepository<ChoiceQuestion>().Where(a => a.Left > choiceQuestion.Left && a.Right < choiceQuestion.Right && a.Value.HasValue).ToList();
                List<int> fullChoiceQuestionIDList = fullChoiceQuestionList.Select(t => t.ID).ToList();
                List<PatientChoice> patientChoiceList = freeSQL.Select<PatientChoice>().Where(a => a.PatientID == patientID && fullChoiceQuestionIDList.Contains(a.ChoiceQuestionID)).ToList();

                result = APIResult.Success("患者病情分级获取成功", new
                {
                    triage.ID,
                    triage.PatientCondition,
                    patientChoiceList,
                    PatientConditionParentID = (int)QuestionRankID.PatientCondition,
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
        public APIResult PremorbidMRSRankGet(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == patientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }
                Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                ChoiceQuestion choiceQuestion = freeBaseSQL.GetRepository<ChoiceQuestion>().Where(a => a.ID == (int)QuestionRankID.PremorbidMRSRank && a.Layer == 2).First();

                //获取患者选择的内容
                List<ChoiceQuestion> fullChoiceQuestionList = freeBaseSQL.GetRepository<ChoiceQuestion>().Where(a => a.Left > choiceQuestion.Left && a.Right < choiceQuestion.Right && a.Value.HasValue).ToList();
                List<int> fullChoiceQuestionIDList = fullChoiceQuestionList.Select(t => t.ID).ToList();
                List<PatientChoice> patientChoiceList = freeSQL.Select<PatientChoice>().Where(a => a.PatientID == patientID && fullChoiceQuestionIDList.Contains(a.ChoiceQuestionID)).ToList();

                result = APIResult.Success("患者发病前MRS评分获取成功", new
                {
                    triage.ID,
                    triage.PremorbidMRSRank,
                    patientChoiceList,
                    PatientConditionParentID = (int)QuestionRankID.PatientCondition,
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

        public APIResult VitalSignsEdit(VitalSigns vitalSigns)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == vitalSigns.PatientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }

                VitalSigns selectedVitalSigns = freeSQL.GetRepository<VitalSigns>().Where(a => a.PatientID == patient.ID).First();
                if (selectedVitalSigns == null)
                {
                    freeSQL.GetRepository<VitalSigns>().Insert(selectedVitalSigns);
                }
                else
                {
                    freeSQL.GetRepository<VitalSigns>().Attach(selectedVitalSigns);
                    selectedVitalSigns = BaseUtil.XmlDeepCopy(vitalSigns);
                    freeSQL.GetRepository<VitalSigns>().Update(selectedVitalSigns);
                }

                //修改首页显示内容，是否已填写所有数据              
                Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                triage.VitalSigns = selectedVitalSigns.GetFillingStatus();
                freeSQL.GetRepository<Triage>().Update(triage);

                new LogUtil().Add(LogCode.修改, "患者:" + patient.ID + "生命体征成功修改", loginUser);
                result = APIResult.Success("患者生命体征修改成功", selectedVitalSigns);
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
        public APIResult VitalSignsGet(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == patientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }
                VitalSigns selectedVitalSigns = freeSQL.GetRepository<VitalSigns>().Where(a => a.PatientID == patient.ID).First();
                result = APIResult.Success("患者生命体征获取成功", selectedVitalSigns);
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

        public APIResult RapidBloodGLUEdit(Triage triage)
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
                selectedTriage.RapidBloodGLU = triage.RapidBloodGLU;
                selectedTriage.RapidBloodGLUTime = triage.RapidBloodGLUTime;

                freeSQL.GetRepository<Triage>().Update(selectedTriage);

                new LogUtil().Add(LogCode.修改, "患者:" + patient.ID + "快速血糖成功修改", loginUser);
                result = APIResult.Success("患者快速血糖修改成功", triage);
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
        public APIResult RapidBloodGLUGet(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == patientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }
                Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                result = APIResult.Success("患者快速血糖获取成功", new
                {
                    triage.ID,
                    triage.RapidBloodGLU,
                    triage.RapidBloodGLUTime
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



        public APIResult EstablishVeinPassageEdit(Triage triage)
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
                selectedTriage.IsEstablishVeinPassage = triage.IsEstablishVeinPassage;

                if (triage.IsEstablishVeinPassage.HasValue && triage.IsEstablishVeinPassage.Value)
                {
                    selectedTriage.EstablishVeinPassageTime = triage.EstablishVeinPassageTime;
                }

                freeSQL.GetRepository<Triage>().Update(selectedTriage);

                new LogUtil().Add(LogCode.修改, "患者:" + patient.ID + "开通静脉通道成功修改", loginUser);
                result = APIResult.Success("患者开通静脉通道修改成功", triage);
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
        public APIResult EstablishVeinPassageGet(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == patientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }
                Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                result = APIResult.Success("患者开通静脉通道获取成功", new
                {
                    triage.ID,
                    triage.IsEstablishVeinPassage,
                    triage.EstablishVeinPassageTime
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

        public APIResult WeightEdit(Triage triage)
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
                selectedTriage.Weight = triage.Weight;
                freeSQL.GetRepository<Triage>().Update(selectedTriage);

                new LogUtil().Add(LogCode.修改, "患者:" + patient.ID + "体重成功修改", loginUser);
                result = APIResult.Success("患者体重修改成功", triage);
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
        public APIResult WeightGet(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == patientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }
                Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                result = APIResult.Success("患者体重获取成功", new
                {
                    triage.ID,
                    triage.Weight
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

        public APIResult CollectBloodTimeEdit(Triage triage)
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
                selectedTriage.CollectBloodTime = triage.CollectBloodTime;
                freeSQL.GetRepository<Triage>().Update(selectedTriage);

                new LogUtil().Add(LogCode.修改, "患者:" + patient.ID + "采血时间成功修改", loginUser);
                result = APIResult.Success("患者采血时间修改成功", triage);
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
        public APIResult CollectBloodTimeGet(int patientID)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {
                Patient patient = freePatientSQL.GetRepository<Patient>().Where(a => a.ID == patientID).First();
                if (patient == null)
                {
                    throw new ExceptionUtil("未找到该患者");
                }
                Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patient.ID).First();
                result = APIResult.Success("患者采血时间获取成功", new
                {
                    triage.ID,
                    triage.CollectBloodTime
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
    }
}
