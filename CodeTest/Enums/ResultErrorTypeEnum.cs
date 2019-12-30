using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CodeTest.Enums
{
    public enum ResultErrorTypeEnum
    {
        [Description("Element Not Found!")]
        NotFoundError,
        [Description("Invalid Input!")]
        ValidationError,
        [Description("Server Error!")]
        Exception
    }
}
