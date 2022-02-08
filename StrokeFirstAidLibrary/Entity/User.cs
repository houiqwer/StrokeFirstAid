using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColumnAttribute = FreeSql.DataAnnotations.ColumnAttribute;
using TableAttribute = FreeSql.DataAnnotations.TableAttribute;

namespace StrokeFirstAidLibrary.Entity
{
    [Table]
    public class User
    {
        [Column(IsIdentity = true)]
        public int ID { get; set; }
        public string Username { get; private set; } = string.Empty;
        public string RealName { get; set; } = string.Empty;
        public string Cellphone { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public UserType UserType { get; set; }
        public DateTime? Expire { get; set; }
        public bool? IsDelete { get; set; }
        public string VerificationCode { get; private set; } = string.Empty;
        public DateTime VerificationCodeTime { get; private set; }

        public User()
        {
            Username = Guid.NewGuid().ToString();
        }

        public void SetVerificationCode()
        {
            VerificationCode = GetVerificationCode(6);
            VerificationCodeTime = DateTime.Now;
        }


        public string GetVerificationCode(int codecount)
        {
            string allChar = "1,2,3,4,5,6,7,8,9,0,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            Random random = new Random();
            for (int i = 0; i < codecount; i++)
            {
                random = new Random((i + 1) * ((int)System.DateTime.Now.Ticks));
                int t = random.Next(36);
                randomCode += allCharArray[t];
            }
            return randomCode;
        }
    }




    public enum UserType
    {
        患者 = 1,
        社区工作者 = 2,
        卒中医生 = 3,

        其他 = 9
    }
}
