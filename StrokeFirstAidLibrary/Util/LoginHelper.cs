using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using StrokeFirstAidLibrary.Entity;
using static StrokeFirstAidLibrary.StrokeFirstAidDBContext;

namespace StrokeFirstAidLibrary.Util
{
    public class LoginHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public LoginHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static User? CurrentUser()
        {
            //string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"][0];
            //return freeSQL.Select<User>().Where(a => a.Token == token).ToOne();
            return null;
        }

        public static string GenerateToken(int userID, DateTime expire)
        {
            User user = freeBaseSQL.GetRepository<User>().Where(a => a.ID == userID).First();
            if (user != null)
            {
                if (string.IsNullOrEmpty(user.Token))
                    user.Token = Guid.NewGuid().ToString();
                user.Expire = expire;
                freeBaseSQL.GetRepository<User>().Update(user);
            }
            return user.Token;
        }
    }
}
