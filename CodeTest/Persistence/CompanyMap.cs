using CodeTest.Model;
using MongoDB.Bson.Serialization;

namespace CodeTest.Persistence
{
    public class CompanyMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Company>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(false);
                map.MapIdMember(x => x.ID);
                map.MapMember(x => x.Name).SetIsRequired(true);
                map.MapMember(x => x.NumberOfEmployees).SetIsRequired(true);
                map.SetExtraElementsMember(map.GetMemberMap(c => c.AddedColumns));
            });

            
        }
    }
}