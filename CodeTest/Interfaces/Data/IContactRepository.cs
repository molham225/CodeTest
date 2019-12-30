using CodeTest.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeTest.Interfaces
{
    public interface IContactRepository : IRepository<Contact>
    {
        //Task<IEnumerable<Contact>> GetFiltered(List<ColumnFilterInfo> filters, PaginationInfo paginationInfo);
    }
}
