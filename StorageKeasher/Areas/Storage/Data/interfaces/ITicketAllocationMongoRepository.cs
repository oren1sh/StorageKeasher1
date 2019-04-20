using Asat_Technion.Areas.Tickets.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asat_Technion.Areas.Tickets.Data.interfaces
{
    interface ITicketAllocationMongoRepository
    {
        Task<List<TicketAllocationMongo>> GetAllTicketAllocationsByLandingPageIdAsync(string landingPageId);
        Task<bool> UpdateAmountAsync(TicketAllocationMongo ticketAllocationMongo);
    }//end interface
}//end namespace
