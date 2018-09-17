using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using ParadiseExplorer.Models;

namespace ParadiseExplorer.Profiles
{
    public static class BsonMapping
    {
        public static void MapModels()
        {
            BsonClassMap.RegisterClassMap<EntityDto>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(x => x.Name).SetElementName("\"name\"");
                cm.MapMember(x => x.NodeId).SetElementName("\"node_id\"");
                cm.MapMember(x => x.Jurisdiction).SetElementName("\"jurisdiction\"");
                cm.MapMember(x => x.JurisdictionDescription).SetElementName("\"jurisdictionDescription\"");
                cm.MapMember(x => x.Countries).SetElementName("\"countries\"");
                cm.MapMember(x => x.CountryCodes).SetElementName("\"countryCodes\"");
            });

            BsonClassMap.RegisterClassMap<EdgeDto>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(x => x.From).SetElementName("\"START_ID\"");
                cm.MapMember(x => x.To).SetElementName("\"END_ID\"");
                cm.MapMember(x => x.Type).SetElementName("\"TYPE\"");
                cm.MapMember(x => x.Link).SetElementName("\"link\"");
                //cm.MapMember(x => x.EdgeTypeDesc).SetElementName("\"TYPE\"");
                //cm.MapMember(x => x.EdgeType).SetSerializer()
            });

            BsonClassMap.RegisterClassMap<OfficerDto>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(x => x.NodeId).SetElementName("\"node_id\"");
                cm.MapMember(x => x.Name).SetElementName("\"name\"");
                cm.MapMember(x => x.CountryCodes).SetElementName("\"country_codes\"");
                cm.MapMember(x => x.Countries).SetElementName("\"countries\"");
            });

            BsonClassMap.RegisterClassMap<AddressDto>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(x => x.NodeId).SetElementName("\"node_id\"");
                cm.MapMember(x => x.Name).SetElementName("\"name\"");
                cm.MapMember(x => x.Address1).SetElementName("\"address\"");
                cm.MapMember(x => x.CountryCodes).SetElementName("\"country_codes\"");
                cm.MapMember(x => x.Countries).SetElementName("\"countries\"");
            });
        }
    }
}
