using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeFirstAidLibrary.Util
{
    public class ExceptionUtil : ApplicationException
    {
        private string error;
        private Exception innerException;
        //无参数构造函数
        public ExceptionUtil()
        {

        }
        //带一个字符串参数的构造函数，作用：当程序员用Exception类获取异常信息而非 MyException时把自定义异常信息传递过去
        public ExceptionUtil(string message)
            : base(message)
        {
            error = message;
        }
        //带有一个字符串参数和一个内部异常信息参数的构造函数
        public ExceptionUtil(string message, Exception innerException)
            : base(message)
        {
            this.innerException = innerException;
            this.error = message;
        }
        public string GetError()
        {
            return error;
        }
    }
}
