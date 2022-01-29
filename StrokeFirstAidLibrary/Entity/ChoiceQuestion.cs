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
    //包含所有树形选项内容，根节点从1开始，层级为1；可选菜单层级为2
    public class ChoiceQuestion
    {     
        [Column(IsIdentity = true)]
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Value { get; set; }

        public int Left { get; set; }
        public int Right { get; set; }
        public int Layer { get; set; }
    }

    
}
