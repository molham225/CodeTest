using CodeTest.Interfaces;
using CodeTest.Model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Repository
{
    public class SequenceRepository:ISequenceRepository
    {
        private IMongoDatabase database;
        private MongoClient mongoClient;

        public SequenceRepository(IConfiguration configuration) {
            this.mongoClient = new MongoClient(configuration["MongoSettings:Connection"]);
            this.database = mongoClient.GetDatabase(configuration["MongoSettings:DatabaseName"]);
        }
        public void Insert(Sequence sequence)
        {
            var collection = database.GetCollection<Sequence>("sequence");
            collection.InsertOne(sequence);
        }

        public long GetNextSequenceValue( string sequenceName)
        {
            var collection = database.GetCollection<Sequence>("sequence");
            var filter = Builders<Sequence>.Filter.Eq(a => a.Name, sequenceName);
            Sequence  seq = collection.Find(filter).FirstOrDefault();
            if(seq == null)
            {
                seq = new Sequence();
                seq.Name = sequenceName;
                seq.Value = 1;
                collection.InsertOne(seq);
            }
            var update = Builders<Sequence>.Update.Inc(a => a.Value, 1);
            var sequence = collection.FindOneAndUpdate(filter, update);

            return sequence.Value;
        }
    }
}
