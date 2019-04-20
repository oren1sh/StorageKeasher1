using StorageKeasher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StorageKeasher.Areas.Storage.Data.interfaces
{
    public interface IItemMongoRepository
    {
        //get all items
        Task<List<ItemMongo>> GetAllItemsAsync();
        //get by personal number
        Task<List<ItemMongo>> GetAllItemsByPNAsync(string PN);
        //get by item id
        Task<ItemMongo> GetItemById(long id);
        //get by item IDF id
        Task<ItemMongo> GetItemByIdfId(string IdfId);
        //create new item entry
        Task Create(ItemMongo todo);
        //create many items
        Task<bool> CreateManyAsync(ItemMongo todo , int HowMany);
        // update item
        Task<bool> UpdateAsync(ItemMongo todo);
        // delete item
        Task<bool> DeleteAsync(long id);

        
    }
}
