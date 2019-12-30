using CodeTest.Interfaces;
using CodeTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Repository
{
    public class CompanyRepository:BaseRepository<Company>,ICompanyRepository
    {
        private readonly IMongoContext context;
        private readonly ISequenceRepository sequence;
        public CompanyRepository(ISequenceRepository sequence,IMongoContext context) : base(context)
        {
            this.context = context;
            this.sequence = sequence;
        }
        public override async Task<Company> Add(Company company)
        {
            company.ID = sequence.GetNextSequenceValue(typeof(Company).Name);
            return await base.Add(company);
        }
    }
}
