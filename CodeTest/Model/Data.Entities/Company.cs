using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Model
{
    public class Company/*:BaseEntity<int>*/
    {
        [BsonId]
        public long ID { set; get; }
        public string Name { set; get; }
        public int NumberOfEmployees { set; get; }
        [BsonExtraElements]
        public BsonDocument AddedColumns { get; set; } = new BsonDocument();
    }
}
