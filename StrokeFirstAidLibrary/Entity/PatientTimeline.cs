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
    [Index("uk_patient_timeline_index", "PatientID,TimelineID", true)]
    [Table]
    public class PatientTimeline
    {
        [Column(IsIdentity = true)]
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int TimelineID { get; set; }
        [Column(ServerTime = DateTimeKind.Utc)]
        public DateTime Date { get; set; }
        public string Remark { get; set; } = string.Empty;
    }


}
