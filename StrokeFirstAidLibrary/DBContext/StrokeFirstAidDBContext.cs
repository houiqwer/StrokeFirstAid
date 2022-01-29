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
            .UseSlave("Data Source=(local);Initial Catalog=StrokeFirstAidSlave1;Encrypt=True;TrustServerCertificate=True;User ID=sa;Password=123456;", "Data Source=(local);Initial Catalog=StrokeFirstAidSlave2;Encrypt=True;TrustServerCertificate=True;User ID=sa;Password=123456;") //使用从数据库，支持多个
            .UseAutoSyncStructure(true) //自动同步实体结构到数据库
            .Build();

        //public ISelect<ChoiceQuestion> choiceQuestionSelect => freeSQL.Select<ChoiceQuestion>();

        public StrokeFirstAidDBContext()
        {

        }

    }


}
