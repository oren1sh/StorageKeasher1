using MongoDB.Driver;
using StorageKeasher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageKeasher.Data.interfaces
{
    public interface IStorageContext
    {
        IMongoCollection<ItemMongo> ItemsCollection { get; }
        IMongoCollection<Man> MansCollection { get; }
        IMongoCollection<ItemBag> ItemBagsCollection { get; }
        
    }
}
