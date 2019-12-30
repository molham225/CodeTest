using CodeTest.Interfaces;
using CodeTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Repository
{
    public class AddedColumnRepository:BaseRepository<AddedColumn>, IAddedColumnRepository
    {
        public AddedColumnRepository(IMongoContext context):base(context)
        {
                            
        }
    }
}
