using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ParadiseExplorer.Models
{
    [BsonIgnoreExtraElements]
    public class EdgeDto
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Id { get; set; }
        public int From { get; set; }
        public string Type { get; set; }
        public int To { get; set; }
        public string Link { get; set; }
        public EdgeType EdgeType { get; set; }
        public string EdgeTypeDesc { get; set; }
    }
}
