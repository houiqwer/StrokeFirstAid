using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeSql;
using FreeSql.DataAnnotations;
using ColumnAttribute = FreeSql.DataAnnotations.ColumnAttribute;
using TableAttribute = FreeSql.DataAnnotations.TableAttribute;

namespace StrokeFirstAidLibrary.Entity
{
    [Table]
    public class OutcomeDelay : PatientRecord
    {
        public bool? IsDelay { get; set; }
        public DelayReason? DelayReason { get; set; }

        public string? OtherReason { get; set; }

        public FillingStatus GetFillingStatus()
        {
            if (!IsDelay.HasValue)
            {
                return FillingStatus.未填写;
            }         

            return FillingStatus.已完成;
        }

    }

    public enum DelayReason
    {
        症状不明显延误诊断 = 1,
        家属未到场 = 2,
        医生决策延误 = 3,
        排队挂号缴费时间长 = 4,
        问诊时间长 = 5,
        治疗期间出现并发症 = 6,
        知情同意时间过长 = 7,
        未实施绕行急诊方案 = 8,
        影像检查等待时间长 = 9,
        药物缺乏 = 10,
        超过在关注时间窗 = 11,
        病情不稳定 = 12,
        导管室占台 = 13,
        缺少担架员转运时间长 = 14,
        经费问题 = 15,
        会诊时间长 = 16,

        其他原因 = 99
    }

}
