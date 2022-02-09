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
    public class VitalSigns : PatientRecord
    {
        public decimal? Temperature { get; set; }
        public int? Pulse { get; set; }
        public int? Breathing { get; set; }
        public decimal? SBP { get; set; }
        public decimal? DBP { get; set; }
        public Consciousness? Consciousness { get; set; }

        public FillingStatus GetFillingStatus()
        {
            if (Temperature == null && Pulse == null && Breathing == null && SBP == null && SBP == null && Consciousness == null)
            {
                return FillingStatus.未填写;
            }

            if (Temperature == null || Pulse == null || Breathing == null || SBP == null || SBP == null || Consciousness == null)
            {
                return FillingStatus.部分填写;
            }

            return FillingStatus.已完成;
        }
    }

    public enum Consciousness
    {
        清醒 = 1,
        模糊 = 2,
        谵妄 = 3,
        嗜睡 = 4,
        昏睡 = 5,
        浅昏迷 = 6,
        中昏迷 = 7,
        深昏迷 = 8
    }
}
