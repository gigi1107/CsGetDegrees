using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using WDown.Models;
using WDown.Views.Character;
using System.Linq;


namespace WDown.Services
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
        Task<bool> InsertUpdateAsync_Character(Character item);
        Task<bool> UpdateAsync_Character(Character character);
        Task<bool> DeleteAsync_Character(Character id);
        Task<Character> GetAsync_Character(string id);
        Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false);

        //Monster
        Task<bool> AddAsync_Monster(Monster monster);
        Task<bool> InsertUpdateAsync_Monster(Monster item);
        Task<bool> UpdateAsync_Monster(Monster monster);
        Task<bool> DeleteAsync_Monster(Monster id);
        Task<Monster> GetAsync_Monster(string id);
        Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false);

        //Score
        Task<bool> AddAsync_Score(Score monster);
        Task<bool> InsertUpdateAsync_Score(Score item);
        Task<bool> UpdateAsync_Score(Score monster);
        Task<bool> DeleteAsync_Score(Score id);
        Task<Monster> GetAsync_Score(string id);
        Task<IEnumerable<Monster>> GetAllAsync_Score(bool forceRefresh = false);

    }
}
