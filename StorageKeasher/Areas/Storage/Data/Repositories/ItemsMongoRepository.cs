using StorageKeasher.Areas.Storage.Data.interfaces;
using StorageKeasher.Data.interfaces;
using StorageKeasher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageKeasher.Areas.Storage.Data.Repositories
{
    public class ItemsMongoRepository : IItemMongoRepository
    {

        private readonly IStorageContext _context;

        public ItemsMongoRepository(IStorageContext context)
        {
            _context = context;
        }

        public Task Create(ItemMongo todo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateManyAsync(ItemMongo todo, int HowMany)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ItemMongo>> GetAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ItemMongo>> GetAllItemsByPNAsync(string PN)
        {
            throw new NotImplementedException();
        }

        public Task<ItemMongo> GetItemById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemMongo> GetItemByIdfId(string IdfId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ItemMongo todo)
        {
            throw new NotImplementedException();
        }
    }
}
