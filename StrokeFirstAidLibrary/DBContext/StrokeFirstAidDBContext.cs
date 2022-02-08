using System;
using System.Configuration;
using System.Linq;
using StrokeFirstAidLibrary.Entity;
using System.IO;
using Microsoft.Extensions.Configuration;
using FreeSql;

namespace StrokeFirstAidLibrary
{
    public sealed class StrokeFirstAidDBContext
    {
        public static IFreeSql freeSQL = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(DataType.SqlServer, "Data Source=(local);Initial Catalog=StrokeFirstAid;Encrypt=True;TrustServerCertificate=True;User ID=sa;Password=123456;")
            .UseSlave("Data Source=(local);Initial Catalog=StrokeFirstAidSlave1;Encrypt=True;TrustServerCertificate=True;User ID=sa;Password=123456;", "Data Source=(local);Initial Catalog=StrokeFirstAidSlave2;Encrypt=True;TrustServerCertificate=True;User ID=sa;Password=123456;")
            .UseAutoSyncStructure(true)
            .Build();

        public static IFreeSql freePatientSQL = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(DataType.SqlServer, "Data Source=(local);Initial Catalog=StrokePatient;Encrypt=True;TrustServerCertificate=True;User ID=sa;Password=123456;")
            .UseAutoSyncStructure(true)
            .Build();

        public static IFreeSql freeBaseSQL = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(DataType.SqlServer, "Data Source=(local);Initial Catalog=StrokeBase;Encrypt=True;TrustServerCertificate=True;User ID=sa;Password=123456;")
            .UseAutoSyncStructure(true)
            .Build();

        //public ISelect<ChoiceQuestion> choiceQuestionSelect => freeSQL.Select<ChoiceQuestion>();

        public StrokeFirstAidDBContext()
        {

        }

    }


}
