using CodeTest.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Model
{
    public class ResultModel<T> where T : class
    {
        public string Status { set; get; }
        public ResultStatusEnum ResultStatus { set; get; }
        public int Code { set; get; }
        public T Data { set; get; }

        public List<ResultErrorModel> Errors { set; get; }

        public static ResultModel<T> GetExceptionResult(ResultErrorModel error)
        {
            return new ResultModel<T>()
            {
                ResultStatus = ResultStatusEnum.ServerError,
                Errors = new List<ResultErrorModel>() { error }
            };
        }

        public static ResultModel<T> GetSuccessResult(T Data = null)
        {
            return new ResultModel<T>()
            {
                ResultStatus = ResultStatusEnum.Success,
                Data = Data
            };
        }

        public static ResultModel<T> GetFailureResult(List<ResultErrorModel> Errors = null)
        {
            return new ResultModel<T>()
            {
                ResultStatus = ResultStatusEnum.BadRequest,
                Errors = Errors
            };
        }

        public static ResultModel<T> GetErrorResult(List<ResultErrorModel> Errors = null)
        {
            return new ResultModel<T>()
            {
                ResultStatus = ResultStatusEnum.BadRequest,
                Errors = Errors
            };
        }

        public static ResultModel<T> GetErrorResult(ResultErrorModel Error = null)
        {
            return new ResultModel<T>()
            {
                ResultStatus = ResultStatusEnum.BadRequest,
                Errors = new List<ResultErrorModel>() { Error }
            };
        }

    }
}
