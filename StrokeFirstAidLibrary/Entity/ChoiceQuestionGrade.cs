using FreeSql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColumnAttribute = FreeSql.DataAnnotations.ColumnAttribute;
using TableAttribute = FreeSql.DataAnnotations.TableAttribute;

namespace StrokeFirstAidLibrary.Entity
{
    [Table]
    public class ChoiceQuestionGrade
    {
        [Column(IsIdentity = true)]
        public int ID { get; set; }
        public int ChoiceQuestionID { get; set; }
        public int Max { get; set; }
        public string Message { get; set; } = string.Empty;
    }

}
