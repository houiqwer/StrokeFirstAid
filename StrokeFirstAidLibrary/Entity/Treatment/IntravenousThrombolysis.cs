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
    public class IntravenousThrombolysis : PatientRecord
    {
        public DateTime? ReceivingTime { get; set; }
        public int? ReceivingDoctorID { get; set; }


        public IntravenousThrombolysis()
        {

        }
        public IntravenousThrombolysis(int patientID)
        {
            PatientID = patientID;
        }

    }
}
