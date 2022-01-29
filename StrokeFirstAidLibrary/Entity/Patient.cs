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
        //[Column(IsNullable = false)]
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public Sex Sex { get; set; }

        public string IDNumber { get; set; } = string.Empty;

        public bool HasImage { get; set; } = false;//public int PatientID { get; set; }
        public string? ImageCode { get; set; }
        public Relation EmergencyContactRelation { get; set; }
        public string EmergencyContact { get; set; } = string.Empty;
        public string EmergencyContactNumber { get; set; } = string.Empty;
        //[Column(ServerTime = DateTimeKind.Utc, CanUpdate = false)]
        //public DateTime CreateDate { get; set; }

        public string Number { get; set; } = string.Empty;
    }

    public enum Relation
    {
        亲属 = 1,
        朋友 = 2,
        同事 = 3,
        其他 = 4
    }

    public enum Sex
    {
        男 = 0,
        女 = 1
    }
}
