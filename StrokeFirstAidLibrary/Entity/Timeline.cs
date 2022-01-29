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
    public class Timeline
    {
        [Column(IsIdentity = true)]
        public int ID { get; set; }
        public int Number { get; set; }
        public string Name { get; set; } = string.Empty;

        [Column(IsIgnore = true)]
        public bool IsRemain { get; set; } = false;
    }


}
