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
    public class Patient
    {
        [Column(IsIdentity = true)]
        public int ID { get; set; }      
        public string Number { get; private set; } = string.Empty;

        [Column(ServerTime = DateTimeKind.Utc, CanUpdate = false)]
        public DateTime CreateDate { get; set; }

        public Patient()
        {
            Number = ((int)((DateTime.Now.Date - Convert.ToDateTime("2022-01-01").Date).TotalDays)).ToString().PadLeft(4, '0') + "-" + DateTime.Now.ToString("HHmmddfff");
        }
    } 
}
