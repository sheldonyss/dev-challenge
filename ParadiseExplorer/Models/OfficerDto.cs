using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParadiseExplorer.Models
{
    [BsonIgnoreExtraElements]
    public class OfficerDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int NodeId { get; set; }
        public string Name { get; set; }
        public string CountryCodes { get; set; }
        public string Countries { get; set; }
    }
}
