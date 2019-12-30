using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Model
{
    public class BaseEntity<T>
    {
        [BsonId]
        T ID { set; get; }
    }
}
