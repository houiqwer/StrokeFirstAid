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
    public class Examination : PatientRecord
    {
        public int? FirstNIHSSRank { get; set; }
        public PrimaryDiagnosis? PrimaryDiagnosis { get; set; }
        public bool? IsLVO { get; set; }
        public FillingStatus PreliminaryTreatment { get; set; } = FillingStatus.未填写;
        public FillingStatus Referral { get; set; } = FillingStatus.未填写;
        public FillingStatus MedicalRecord { get; set; } = FillingStatus.未填写;
        public int? FirstGCSRank { get; set; }
        public bool? IsCranialCT { get; set; }
        public DateTime CranialCTTime { get; set; }
        public FillingStatus ImagingExamination {get;set; } = FillingStatus.未填写;
        public FillingStatus ASPECTSRank { get; set; } = FillingStatus.未填写;
        public FillingStatus LaboratoryExamination { get; set; } = FillingStatus.未填写;

        public Examination()
        {

        }
        public Examination(int patientID)
        {
            PatientID = patientID;
        }

    }

    public enum PrimaryDiagnosis
    {
        缺血性卒中 = 0,
        出血性卒中 = 1,
        未分类卒中 = 2,
        非卒中 = 9
    }
}
