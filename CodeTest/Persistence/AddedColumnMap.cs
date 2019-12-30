using CodeTest.Model;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Persistence
{
    public class AddedColumnMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<AddedColumn>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdMember(x => x.ID);
                map.MapMember(x => x.EntityName).SetIsRequired(true);
                map.MapMember(x => x.ColumnName).SetIsRequired(true);
                map.MapMember(x => x.ColumnType).SetIsRequired(true);
            });


        }
    }
}
