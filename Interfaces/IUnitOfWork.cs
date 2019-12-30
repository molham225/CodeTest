using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        Task<bool> RollBack();
    }
}
