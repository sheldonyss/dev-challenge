using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParadiseExplorer.Models
{
    [BsonIgnoreExtraElements]
    public class EntityDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int NodeId { get; set; }
        public string Name { get; set; }
        public string Jurisdiction { get; set; }
        public string JurisdictionDescription { get; set; }
        public string CountryCodes { get; set; }
        public string Countries { get; set; }
    }
}
