using StrokeFirstAidLibrary.Entity;
using static StrokeFirstAidLibrary.StrokeFirstAidDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrokeFirstAidLibrary.Util
{
    public class QuestionRankUtil
    {
        public void Rank(QuestionRankID questionRank, int patientID, int? score)
        {
            switch (questionRank)
            {
                case QuestionRankID.FASTEDRank:
                    {
                        Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patientID).First();
                        if (score.HasValue)
                        {
                            triage.FASTEDRank = score;
                        }
                        else
                        {
                            triage.FASTEDRank = null;
                        }
                        freeSQL.GetRepository<Triage>().Update(triage);
                        break;
                    }
                case QuestionRankID.PatientCondition:
                    {
                        Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patientID).First();
                        if (score.HasValue)
                        {
                            triage.PatientCondition = (PatientCondition)score;
                        }
                        else
                        {
                            triage.PatientCondition = null;
                        }
                        freeSQL.GetRepository<Triage>().Update(triage);
                        break;
                    }
                case QuestionRankID.PremorbidMRSRank:
                    {
                        Triage triage = freeSQL.GetRepository<Triage>().Where(a => a.PatientID == patientID).First();
                        if (score.HasValue)
                        {
                            triage.PremorbidMRSRank = score;
                        }
                        else
                        {
                            triage.PremorbidMRSRank = null;
                        }
                        freeSQL.GetRepository<Triage>().Update(triage);
                        break;
                    }
                default:
                    throw new ExceptionUtil("评分类型错误");
                    //break;
            }
        }
    }
}
