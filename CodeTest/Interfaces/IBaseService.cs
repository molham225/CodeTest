using CodeTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Interfaces
{
    public interface IBaseService<TDto, TCreateModel, TUpdateModel, TKey> where TDto : class
    {
        ResultModel<IEnumerable<TDto>> GetAll();
        ResultModel<TDto> GetById(TKey id);
        ResultModel<TDto> Create(TCreateModel model);
        Task<ResultModel<TDto>> CreateAsync(TCreateModel model);
        ResultModel<TDto> Update(TUpdateModel model);
        Task<ResultModel<TDto>> UpdateAsync(TUpdateModel model);
        ResultModel<object> Delete(TKey id);
        Task<ResultModel<object>> DeleteAsync(TKey id);
    }
}

