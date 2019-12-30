using CodeTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Services
{
    public class SeedDbService : ISeedDbService
    {
        private readonly IContactRepository _contact;
        private readonly IAddedColumnRepository _addedColunmn;
        public SeedDbService(IContactRepository _contact, IAddedColumnRepository _addedColunmn)
        {
            this._contact = _contact;
            this._addedColunmn = _addedColunmn;
        }
        public void Seed()
        {
            _contact.CreateUniqueIndex("Name");
            _contact.CreateUniqueIndex("Company.Name");
        }
    }
}
