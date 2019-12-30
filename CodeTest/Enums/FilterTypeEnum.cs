using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Enums
{
    public enum FilterTypeEnum
    {
        [Description("eq")]
        Equals,
        [Description("gt")]
        GreaterThan,
        [Description("gte")]
        GreaterThanOrEquals,
        [Description("lt")]
        LesserThan,
        [Description("lte")]
        LesserThanOrEquals,
        [Description("regex")]
        Regex

    }
}
