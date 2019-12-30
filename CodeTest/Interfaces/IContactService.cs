using CodeTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Interfaces
{
    public interface IContactService
    {
        Task<ResultModel<List<Dictionary<string,object>>>> GetAll(PaginationInfo paginationInfo);
        Task<ResultModel<Dictionary<string,object>>> GetById(int id);
        Task<ResultModel<Dictionary<string,object>>> Create(Dictionary<string,object> model);
        Task<ResultModel<Dictionary<string,object>>> Update(Dictionary<string,object> model);
        Task<ResultModel<object>> Delete(int id);
        Task<ResultModel<List<Dictionary<string,object>>>> Filter(List<ColumnFilterInfo> filters,PaginationInfo paginationInfo);
    }
}
