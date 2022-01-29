using StrokeFirstAidLibrary;
using StrokeFirstAidLibrary.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeFirstAidLibrary.Util
{
    public class APIResult
    {
        public ApiResultCodeType code { get; set; }
        public string message { get; set; }
        public object? data { get; set; }

        public APIResult()
        {
            code = ApiResultCodeType.None;
            message = "";
            data = null;
        }


        public static APIResult Error(string message, object? data = null)
        {
            return new APIResult()
            {
                code = ApiResultCodeType.Failure,
                message = message,
                data = data
            };
        }


        public static APIResult Success(string message, object? data = null)
        {
            return new APIResult()
            {
                code = ApiResultCodeType.Success,
                message = message,
                data = data
            };
        }

        public enum ApiResultCodeType
        {
            /// <summary>
            /// 成功
            /// </summary>
            [Description("Success")]
            Success = 0,
            /// <summary>
            /// 失败
            /// </summary>
            [Description("Failure")]
            Failure = 1,
            /// <summary>
            /// 无
            /// </summary>
            [Description("None")]
            None = 3,
        }
    }
}
