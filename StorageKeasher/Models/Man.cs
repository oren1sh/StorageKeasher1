using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageKeasher.Models
{
    public class Man
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }

        [BsonElement("Name")]
        [BsonRequired]
        public string Name { get; set; }

        [BsonElement("PN")]
        [BsonRequired]
        public string PN { get; set; }

        [BsonElement("ID")]
        [BsonRequired]
        public string ID { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [BsonElement("Team")]
        public string Team { get; set; }

        [BsonElement("ItemSing")]
        public List<ItemMongo> ItemSing { get; set; }

        [BsonElement("Note")]
        public string Note { get; set; }

    }
}
