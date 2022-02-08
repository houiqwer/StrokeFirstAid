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
    public class PatientInfo
    {
        [Column(IsIdentity = true)]
        public int ID { get; set; }
        public int PatientID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Age { get; set; }
        public Sex? Sex { get; set; }
        public string IDNumber { get; set; } = string.Empty;
        public bool HasImage { get; set; } = false;
        public string? ImageCode { get; set; }
        public Relation? EmergencyContactRelation { get; set; }
        public string EmergencyContact { get; set; } = string.Empty;
        public string EmergencyContactNumber { get; set; } = string.Empty;

        public FillingStatus GetFillingStatus()
        {
            if (string.IsNullOrEmpty(Name) && Age == null && Sex == null && string.IsNullOrEmpty(IDNumber) && EmergencyContactRelation == null && string.IsNullOrEmpty(EmergencyContact) && string.IsNullOrEmpty(EmergencyContactNumber))
            {
                return FillingStatus.未填写;
            }

            if (string.IsNullOrEmpty(Name) || Age == null || Sex == null || string.IsNullOrEmpty(IDNumber) || EmergencyContactRelation == null || string.IsNullOrEmpty(EmergencyContact) || string.IsNullOrEmpty(EmergencyContactNumber))
            {
                return FillingStatus.部分填写;
            }

            return FillingStatus.已完成;
        }

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
