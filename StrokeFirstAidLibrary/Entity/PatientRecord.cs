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
    public class PatientRecord
    {
        [Column(IsIdentity = true)]
        public int ID { get; set; }
        public int PatientID { get; set; }


        [Column(ServerTime = DateTimeKind.Utc, CanUpdate = false)]
        public DateTime CreateDate { get; set; }
    }
}
