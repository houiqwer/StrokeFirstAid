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
    public class CardiogramAbnormalTypeSelect : PatientRecord
    {
        public int CardiogramID { get; set; }
        public CardiogramAbnormalType CardiogramAbnormalType { get; set; }
    }

    public enum CardiogramAbnormalType
    {
        房颤 = 1,
        房扑 = 2,
        左室肥厚 = 3,
        病理性Q波 = 4,
        心肌缺血改变 = 5,
        急性心肌梗死 = 6,
        其他 = 9
    }



}
