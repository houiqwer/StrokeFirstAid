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
    //首页查询和整体情况展示
    [Table(DisableSyncStructure = true)]
    public class PatientRecord
    {
        [Column(IsIdentity = true)]
        public int ID { get; set; }
        public int PatientID { get; set; }
    }


    public enum QuestionRankID
    {
        FASTEDRank = 20,
        PatientCondition = 25,
        PremorbidMRSRank = 33,
        FirstNIHSSRank = 112, 
        FirstGCSRank = 131,
        BeforeThrombolysisNIHSSRank = 210,
        AfterThrombolysisNIHSSRank = 289,
        BeforeInterventionNIHSSRank = 368,
        AfterInterventionNIHSSRank = 447
    }

}
