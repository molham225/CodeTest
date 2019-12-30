using CodeTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Interfaces
{
    public interface ISequenceRepository
    {
        public void Insert(Sequence sequence);

        public long GetNextSequenceValue(string sequenceName);
    }
}
