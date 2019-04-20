using Asat_Technion.Areas.Tickets.Data.interfaces;
using Asat_Technion.Areas.Tickets.Data.Models;
using Asat_Technion.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asat_Technion.Areas.Tickets.Data.Repositories
{
    public class TicketAllocationMongoRepository : ITicketAllocationMongoRepository
    {
        private readonly MongoDbContext mongoDbContext;
        public TicketAllocationMongoRepository(MongoDbContext mongoDbContext)
        {
            this.mongoDbContext = mongoDbContext;
        }//end constructor

        public async Task<List<TicketAllocationMongo>> GetAllTicketAllocationsByLandingPageIdAsync(string landingPageId)
         {
            var list = await mongoDbContext.GetAsync();
            return list.Where(t => t.LandingPageId.Trim().Equals(landingPageId.Trim())).ToList();
         }


        
        public async Task<bool> UpdateAmountAsync(TicketAllocationMongo ticketAllocationMongo)
        {
            var toUpdate = Builders<TicketAllocationMongo>.Filter.Eq("_id", ticketAllocationMongo.Id);
            var result = await mongoDbContext.TicketAllocationCollection.UpdateOneAsync(
                toUpdate,
                Builders<TicketAllocationMongo>.Update
                .Set("CurrentAmount", ticketAllocationMongo.CurrentAmount));
            if (result.ModifiedCount > 0)
                return true;
            else
                return false;
        }//end update amount


    }//end class
}//end namespace
