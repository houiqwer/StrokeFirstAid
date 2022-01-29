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
    public class Triage : PatientRecord
    {
        public FillingStatus? PatientInfo { get; set; }
        public DateTime? DiseaseTime { get; set; }
        public bool? IsWUS { get; set; }
        public DateTime? EmergencyTreatmentTime { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public ArrivalWay? ArrivalWay { get; set; }
        public DateTime? EmergencyReceivingTime { get; set; }
        public DateTime? CSReceivingTime { get; set; }
        public int? CSReceivingDocID { get; set; }
        public int? FASTEDRank { get; set; }
        public PatientCondition? PatientCondition { get; set; }
        public int? PremorbidMRSRank { get; set; }
        public FillingStatus VitalSigns { get; set; }
        public FillingStatus RapidBloodGLU { get; set; }
        public FillingStatus CardiogramCheck { get; set; }
        public bool? IsEstablishVeinPassage { get; set; }
        public DateTime? EstablishVeinPassageTime { get; set; }
        public decimal? Weight { get; set; }
        public DateTime? CollectBloodTime { get; set; }

        public Triage(int patientID)
        {
            PatientID = patientID;
        }
    }


    public enum FillingStatus
    {
        未填写 = 0,
        部分填写 = 1,
        已完成 = 9
    }

    public enum ArrivalWay
    {
        当地120 = 1,
        自行来院 = 2,
        外院转诊 = 3,
        院内发病 = 4,
        其他 = 9
    }

    public enum PatientCondition
    {
        濒危病人 = 1,
        危重病人 = 2,
        急症病人 = 3,
        非急症病人 = 4
    }

}
