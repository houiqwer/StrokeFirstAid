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
    public class Cardiogram : PatientRecord
    {

        public bool HasImage { get; set; } = false;
        public string? ImageCode { get; set; }

        public DateTime? FinishTime { get; set; }
        public bool? IsNormal { get; set; }

        public string? OtherCardiogramAbnormal { get; set; }


        public FillingStatus GetFillingStatus()
        {
            if (HasImage == false && ImageCode == null && FinishTime == null && IsNormal == null)
            {
                return FillingStatus.未填写;
            }

            if (HasImage == false || ImageCode == null || FinishTime == null || IsNormal == null)
            {
                return FillingStatus.部分填写;
            }

            return FillingStatus.已完成;
        }

    }

}
