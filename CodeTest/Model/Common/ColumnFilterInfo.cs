using CodeTest.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Model
{
    public class ColumnFilterInfo
    {
        public string EntityName { set; get; }
        public string ColumnName { set; get; }
        public object Value { get; set; }
        public FilterTypeEnum FilterType { set; get; }
    }
}
