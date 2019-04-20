using MongoDB.Driver;
using StorageKeasher.Config;
using StorageKeasher.Data.interfaces;
using StorageKeasher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageKeasher.Data
{
    public class StorageContext : IStorageContext
    {
        private readonly IMongoDatabase _db;

        public StorageContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }

        public IMongoCollection<ItemMongo> ItemsCollection => _db.GetCollection<ItemMongo>("Items");

        public IMongoCollection<Man> MansCollection => _db.GetCollection<Man>("Mans");

        public IMongoCollection<ItemBag> ItemBagsCollection => _db.GetCollection<ItemBag>("ItemBags");

        //get all by collection
        public async Task<List<ItemMongo>> GetAllAsyncItems()
        {
            var collection = await ItemsCollection.FindAsync(ItemMongo => true);
            return await collection.ToListAsync();
        }

        public async Task<List<Man>> GetAllAsyncMans()
        {
            var collection = await MansCollection.FindAsync(Man => true);
            return await collection.ToListAsync();
        }

        public async Task<List<ItemBag>> GetAllAsyncItemBags()
        {
            var collection = await ItemBagsCollection.FindAsync(ItemBag => true);
            return await collection.ToListAsync();
        }
        //end get all by collection

        //create all by data structe
        public async Task<ItemMongo> CreateAsync(ItemMongo ItemMongo)
        {
            await ItemsCollection.InsertOneAsync(ItemMongo);
            return ItemMongo;
        }

        public async Task<Man> CreateAsync(Man Man)
        {
            await MansCollection.InsertOneAsync(Man);
            return Man;
        }

        public async Task<ItemBag> CreateAsync(ItemBag ItemBag)
        {
            await ItemBagsCollection.InsertOneAsync(ItemBag);
            return ItemBag;
        }
        //end
    }
}
