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
    public class InterventionalTherapy : PatientRecord
    {
        public DateTime? ReceivingTime { get; set; }
        public int? ReceivingDoctorID { get; set; }
        public DateTime? BeginConversationTime { get; set; }
        public bool? IsKnown { get; set; }
        public bool? IsConsent { get; set; }
        public DateTime? SignTime { get; set; }
        public DateTime? FinishConversationTime { get; set; }
        public int? ConversationMinute { get; set; }
        public int? BeforeInterventionNIHSSRank { get; set; }
        public DateTime? BeforeInterventionNIHSSTime { get; set; }
        public bool? IsIntervention { get; set; }
        public int? AfterInterventionNIHSSRank { get; set; }
        public DateTime? AfterInterventionNIHSSTime { get; set; }
        public bool? UntowardReaction { get; set; }

        public InterventionalTherapy()
        {

        }
        public InterventionalTherapy(int patientID)
        {
            PatientID = patientID;
        }

    }
}
