using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Model
{
    public class AddedColumnModel
    {
        public int ID { set; get; }
        public string EntityName { set; get; }
        public string ColumnName { set; get; }
        public string ColumnType { set; get; }
    }
}
