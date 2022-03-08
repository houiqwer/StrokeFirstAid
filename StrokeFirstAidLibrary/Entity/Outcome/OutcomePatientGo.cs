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
    public class OutcomePatientGo : PatientRecord
    {
        public PatientGo? PatientGo { get; set; }
        public int? InpatientNo { get; set; }
        public DateTime? AdmissionTime { get; set; }
        public InpatientDepartment? InpatientDepartment { get; set; }
        public string? OtherPatientGo { get; set; }
    }


    public enum InpatientDepartment
    {

    }

}
