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
    public class Log
    {
        [Column(IsIdentity = true)]
        public int ID { get; set; }     
        public LogCode LogCode { get; set; }
        public string Content { get; set; } = string.Empty;
        public int? UserID { get; set; }
        [Column(ServerTime = DateTimeKind.Utc, CanUpdate = false)]
        public DateTime CreateDate { get; set; }

    }

    public enum LogCode
    {
        成功 = 0,
        登陆 = 1,
        添加 = 2,
        修改 = 3,
        删除 = 4,
        导入 = 5,
        导出 = 6,
        获取 = 7,
     

        登陆错误 = 10001,
        添加错误 = 10002,
        修改错误 = 10003,
        删除错误 = 10004,
        导入错误 = 10005,
        导出错误 = 10006,
        获取错误 = 10007,

        //登陆
        账号或密码错误 = 20101,
        用户被禁用 = 20102,
        用户登陆凭证过期 = 20103,

        //权限
        无权限操作 = 30001,

        //其他
        系统日志 = 90000,
        系统错误 = 90001,
        系统测试 = 90002
    }
}
