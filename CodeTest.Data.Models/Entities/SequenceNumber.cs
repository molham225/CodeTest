using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeTest.Data.Models.Entities
{
    public class SequenceNumber:BaseEntity<ObjectId>
    {
        public string SequenceKey { get; set; }
        public int SequenceValue { get; set; }
    }
}
