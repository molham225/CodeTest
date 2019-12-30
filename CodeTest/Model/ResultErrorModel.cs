using CodeTest.Enums;
using CodeTest.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Model
{
    public class ResultErrorModel
    {
        public string Name { set; get; }
        public string Type { set; get; }
        public string Message { set; get; }

        public ResultErrorModel() { }
        public ResultErrorModel(ResultErrorTypeEnum errorType)
        {
            Name = errorType.ToString();
            Type = errorType.ToString();
            Message = errorType.AsString();
        }
        public ResultErrorModel(Exception e)
        {
            Name = e.GetType().Name;
            Type = ResultErrorTypeEnum.Exception.AsString();
            Message = e.Message;
        }
    }
}
