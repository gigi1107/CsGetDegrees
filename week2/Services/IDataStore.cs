using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using week2.Models;
namespace week2.Services
{
    public interface IDataStore
    {
        //Item
        Task<bool> AddAsync_Item(Item item);
        Task<bool> InsertUpdateAsync_Item(Item item);
        Task<bool> UpdateAsync_Item(Item item);
        Task<bool> DeleteAsync_Item(Item id);
        Task<Item> GetAsync_Item(string id);
        Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false);

        //Character
        Task<bool> AddAsync_Character(Character character);
        Task<bool> UpdateAsync_Character(Character character);
        Task<bool> DeleteAsync_Character(Character id);
        Task<Character> GetAsync_Character(string id);
        Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false);


      
    }
}
