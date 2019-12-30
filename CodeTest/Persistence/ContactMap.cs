using CodeTest.Model;
using MongoDB.Bson.Serialization;

namespace CodeTest.Persistence
{
    public class ContactMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Contact>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(false);
                map.MapIdMember(x => x.ID);
                map.MapMember(x => x.Name).SetIsRequired(true);
                map.MapMember(x => x.Company).SetIsRequired(true);
                map.SetExtraElementsMember(map.GetMemberMap(c => c.AddedColumns));
            });
        }
    }
}