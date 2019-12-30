using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Model
{
    public class Contact/*:BaseEntity<int>*/
    {
        [BsonId]
        public long ID { set; get; }
        public string Name { set; get; }
        public List<Company> Company { set; get; } = new List<Company>();
        [BsonExtraElements]
        public BsonDocument AddedColumns { get; set; } = new BsonDocument();
    }
}
