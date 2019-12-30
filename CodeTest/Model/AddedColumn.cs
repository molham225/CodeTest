using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Model
{
    public class AddedColumn//: BaseEntity <ObjectId>
    {
        [BsonId]
        public int ID { set; get; }
        public string EntityName { set; get; }
        public string ColumnName { set; get; }
        public string ColumnType { set; get; }
    }
}
