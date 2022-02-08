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
    public class AccountBLL
    {
        public APIResult GetVerificationCode(string cellphone)
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            try
            {
                User user = freeBaseSQL.GetRepository<User>().Where(a => a.Cellphone == cellphone).First();
                if (user == null)
                {

                }
                else
                {

                } 
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            if (!string.IsNullOrEmpty(message))
            {
                new LogUtil().Add(LogCode.登陆错误, message);
                result = APIResult.Error(message);
            }

            return result;
        }


        public APIResult Verification()
        {
            APIResult result = new APIResult();
            string message = string.Empty;
            try
            {

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            if (!string.IsNullOrEmpty(message))
            {
                new LogUtil().Add(LogCode.登陆错误, message);
                result = APIResult.Error(message);
            }

            return result;
        }
    }
}
