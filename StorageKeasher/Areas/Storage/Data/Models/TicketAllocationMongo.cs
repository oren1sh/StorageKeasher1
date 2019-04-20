using MongoDB.Bson;

using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asat_Technion.Areas.Tickets.Data.Models
{
    public class TicketAllocationMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [ScaffoldColumn(false)]//don't show in the form
        public string Id { get; set; }

        [Display(Name = "דף נחיתה")]
        [BsonElement("LandingPageId")]
        public string LandingPageId { get; set; }

        [Display(Name = "מזהה DB")]
        [BsonElement("TicketAllocationId")]
        public string TicketAllocationId { get; set; }

        [Display(Name ="קוד זיהוי")]
        [BsonElement("IdentifierId")]
        public string IdentifierId { get; set; }

        [Display(Name = "הקצאות התחלה")]
        [BsonElement("BeginAmount")]
        public int BeginAmount { get; set; }

        [Display(Name = "הקצאות נוכחי")]
        [BsonElement("CurrentAmount")]
        public int CurrentAmount { get; set; }
    }//end class
}//end namespace
