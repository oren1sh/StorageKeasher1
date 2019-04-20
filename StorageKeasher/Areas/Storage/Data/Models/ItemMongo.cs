using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace StorageKeasher.Models
{
    public class ItemMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [ScaffoldColumn(false)]//don't show in the form
        public string Id { get; set; }

        [Display(Name = "שם פריט")]
        [BsonElement("Name")]
        public string ItemName { get; set; }

        [Display(Name = "צלם")]
        [BsonElement("IsIdfId")]
        public bool IsIdfId { get; set; }

        [Display(Name = "מספר צלם")]
        [BsonElement("IdfId")]
        public string IdfId { get; set; }

        [Display(Name = "חתום")]
        [BsonElement("IsSing")]
        public bool IsSing { get; set; }

        [Display(Name = "מ.א. חתום")]
        [BsonElement("SignerId")]
        public string SignerId { get; set; }

        [Display(Name = "הערות")]
        [BsonElement("Note")]
        public string Note { get; set; }
    }
}
