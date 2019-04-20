using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageKeasher.Models
{
    public class ItemBag
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }

        [BsonElement("Name")]
        public string ItemName { get; set; }

        [BsonElement("Items")]
        public List<ItemMongo> Items { get; set; }


    }
}
