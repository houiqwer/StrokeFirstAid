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
    public class Outcome : PatientRecord
    {
        public FillingStatus OutcomeDelay { get; set; } = FillingStatus.未填写;
        public PatientGo? PatientGo { get; set; }
        public bool? IsFiling { get; set; }

        public Outcome()
        {

        }
        public Outcome(int patientID)
        {
            PatientID = patientID;
        }

    }

    public enum PatientGo
    {
        入院 = 1,
        急诊留观 = 2,
        转院 = 3,
        死亡 = 4,
        离园 = 5,
        其他 = 9
    }

}
