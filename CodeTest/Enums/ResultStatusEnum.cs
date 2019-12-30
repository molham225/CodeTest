using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CodeTest.Enums
{
    public enum ResultStatusEnum
    {
        [Description("Success")]
        Success,
        [Description("Server Error")]
        ServerError,
        [Description("BadRequest")]
        BadRequest
    }
}
