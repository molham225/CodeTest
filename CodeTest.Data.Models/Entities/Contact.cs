using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTest.Data.Models.Entities
{
    public class Contact : BaseEntity<int>
    {
        public string Name { set; get; }
        public List<Company> Company { set; get; }
    }
}
