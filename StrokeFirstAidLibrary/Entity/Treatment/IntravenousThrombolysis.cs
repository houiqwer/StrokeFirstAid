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
        public DateTime? BeginConversationTime { get; set; }
        public bool? IsKnown { get; set; }
        public bool? IsConsent { get; set; }
        public DateTime? SignTime { get; set; }
        public DateTime? FinishConversationTime { get; set; }
        public int? ConversationMinute { get; set; }
        public int? BeforeThrombolysisNIHSSRank { get; set; }
        public DateTime? BeforeThrombolysisNIHSSTime { get; set; }
        public bool? IsThrombolysis { get; set; }
        public int? AfterThrombolysisNIHSSRank { get; set; }
        public DateTime? AfterThrombolysisNIHSSTime { get; set; }
        public bool? UntowardReaction { get; set; }

        public IntravenousThrombolysis()
        {

        }
        public IntravenousThrombolysis(int patientID)
        {
            PatientID = patientID;
        }

    }
}
