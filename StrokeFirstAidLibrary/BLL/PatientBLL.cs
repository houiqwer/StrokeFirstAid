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
    public class PatientBLL
    {
        public APIResult Add(Patient patient, ArrivalWay arrivalWay)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            User loginUser = LoginHelper.CurrentUser();
            try
            {               
                freeSQL.GetRepository<Patient>().Insert(patient);
                //添加对应诊断数据
                Triage triage = new Triage(patient.ID);
                triage.ArrivalWay = arrivalWay;
                freeSQL.GetRepository<Triage>().Insert(triage);

                new LogUtil().Add(LogCode.添加, "患者入院:" + patient.ID, loginUser);
                result = APIResult.Success("患者入院成功", patient);
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
    }
}
