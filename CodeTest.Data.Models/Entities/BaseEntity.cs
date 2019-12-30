using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTest.Data.Models.Entities
{
    public class BaseEntity<T>
    {
        [BsonId]
        public T ID { set; get; }
    }
}
