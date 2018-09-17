using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace ParadiseMongoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("paradise");

            var collections = db.GetCollection<BsonDocument>("edges");
            ConvertNodeId(collections);
            //long count = entDoc.CountDocuments(FilterDefinition<BsonDocument>.Empty);

        }

        private static void ConvertNodeId(IMongoCollection<BsonDocument> collections)
        {
            var cursor = collections.Find<BsonDocument>(new BsonDocument()).ToCursor();
            var nodeKey = "\"START_ID\"";
            var writeModels = new List<WriteModel<BsonDocument>>();
            foreach (var doc in cursor.ToEnumerable())
            {
                var nodeIdBsonStr = doc.GetValue(nodeKey);
                if (nodeIdBsonStr.IsString)
                {
                    var node_id = nodeIdBsonStr.AsString.Trim('"');
                    var node_id_val = 0;
                    if (int.TryParse(node_id, out node_id_val))
                    {
                        var filter = Builders<BsonDocument>.Filter.Eq("_id", doc.GetValue("_id").AsObjectId);
                        var newDoc = new BsonDocument(nodeKey, node_id_val);
                        writeModels.Add(new UpdateManyModel<BsonDocument>(filter, new BsonDocument("$set", newDoc)));
                        //collections.UpdateOne(filter, new BsonDocument("$set", newDoc));
                    }
                    else
                    {
                        var filter = Builders<BsonDocument>.Filter.Eq("_id", doc.GetValue("_id").AsObjectId);
                        writeModels.Add(new DeleteManyModel<BsonDocument>(filter));
                    }
                }
                else if (nodeIdBsonStr.IsBsonNull)
                {
                    var filter = Builders<BsonDocument>.Filter.Eq("_id", doc.GetValue("_id").AsObjectId);
                    writeModels.Add(new DeleteManyModel<BsonDocument>(filter));
                }
            }
            collections.BulkWrite(writeModels);
        }
    }
}
