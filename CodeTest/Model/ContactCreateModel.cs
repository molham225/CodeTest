using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Model
{
    public class ContactCreateModel//:ExpandoObject
    {
        public string Name { set; get; }
        public List<CompanyCreateModel> Company { set; get; }
    }
}
