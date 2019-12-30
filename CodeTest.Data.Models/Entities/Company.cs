using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTest.Data.Models.Entities
{
    public class Company:BaseEntity<int>
    {
        public string Name { set; get; }
        public int NumberOfEmployees { set; get; }


    }
}
