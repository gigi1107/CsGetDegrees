using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDown.Models;
using WDown.ViewModels;
using SQLite;

namespace WDown.Services
{
    public sealed class SQLDataStore : IDataStore
    {

        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static SQLDataStore _instance;

        public static SQLDataStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SQLDataStore();
                }
                return _instance;
            }
        }

        private SQLDataStore()
        {
            CreateTables();
            //            InitilizeSeedData();
        }
        public void InitializeDatabaseNewTables()
        {
            DeleteTables();
            CreateTables();

            // Populate them
            InitilizeSeedData();

            // Tell View Models they need to refresh
            NotifyViewModelsOfDataChange();
        }

        // Delete the Datbase Tables by dropping them
        private void DeleteTables()
        {
            App.Database.DropTableAsync<Item>().Wait();
            App.Database.DropTableAsync<BaseCharacter>().Wait();
            App.Database.DropTableAsync<BaseMonster>().Wait();
            App.Database.DropTableAsync<Score>().Wait();
        }

        // Tells the View Models to update themselves.
        private void NotifyViewModelsOfDataChange()
        {
            ItemsViewModel.Instance.SetNeedsRefresh(true);
            MonstersViewModel.Instance.SetNeedsRefresh(true);
            CharactersViewModel.Instance.SetNeedsRefresh(true);
            //ScoresViewModel.Instance.SetNeedsRefresh(true);
        }

        // Create the Database Tables
        private void CreateTables()
        {
            App.Database.CreateTableAsync<Item>().Wait();
            App.Database.CreateTableAsync<BaseCharacter>().Wait();
            App.Database.CreateTableAsync<BaseMonster>().Wait();
            App.Database.CreateTableAsync<Score>().Wait();
        }

        private async void InitilizeSeedData()
        {
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Name = "Fresh Carrot",
                    Description = "Recharge Current Health" },
                new Item { Id = Guid.NewGuid().ToString(), Name = "Wet Grass",
                    Description = "Recharge Current Health, but much less effective than carrots."},
                new Item { Id = Guid.NewGuid().ToString(), Name = "Magical Dew",
                    Description = "Recharge Current Wisdom"}

            };

            //await AddAsync_Item(new Item
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = "Jewels of Gibberish",
            //    Description = "Nonsense Item"
            //});
            //await AddAsync_Item(new Item
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = "Tree Bark",
            //    Description = "Increase Defense"
            //});

            await AddAsync_Character(new Character
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Hazel",
                Description = "Hazel-rah, the leader of Watership Down warren."
            });

            await AddAsync_Character(new Character
            {
                Id = Guid.NewGuid().ToString(),
                Name = "BigWit",
                Description = "The strongest defended of the warren, with a passion of fire."
            });
            


        }

        #region Item
        // Item
        public async Task<bool> InsertUpdateAsync_Item(Item data)
        {

            //// Check to see if the item exist
            //var oldData = await GetAsync_Item(data.Id);
            //if (oldData == null)
            //{
            //    _itemDataset.Add(data);
            //    return true;
            //}

            //// Compare it, if different update in the DB
            //var UpdateResult = await UpdateAsync_Item(data);
            //if (UpdateResult)
            //{
            //    await AddAsync_Item(data);
            //    return true;
            //}

            return false;
        }

        public async Task<bool> AddAsync_Item(Item data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;

        }

        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;

    }

        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

        return false;

    }

        public async Task<Item> GetAsync_Item(string id)
        {
            var result = await App.Database.GetAsync<Item>(id);
            return result;
        }


        public async Task<IEnumerable<Item>> GetAllAsync_Item(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Item>().ToListAsync();
            return result;

        }

        #endregion Item


        // Character
        public async Task<bool> AddAsync_Character(Character data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;

        }
        public async Task<bool> InsertUpdateAsync_Character(Character data)
        {

            //// Check to see if the item exist
            //var oldData = await GetAsync_Character(data.Id);
            //if (oldData == null)
            //{
            //    _characterDataset.Add(data);
            //    return true;
            //}

            //// Compare it, if different update in the DB
            //var UpdateResult = await UpdateAsync_Character(data);
            //if (UpdateResult)
            //{
            //    await AddAsync_Character(data);
            //    return true;
            //}

            return false;
        }
        public async Task<bool> UpdateAsync_Character(Character data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;

        }

        public async Task<bool> DeleteAsync_Character(Character data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

        return false;

         }

        public async Task<Character> GetAsync_Character(string id)
        {
            var result = await App.Database.GetAsync<Character>(id);
            return result;
        }

        public async Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Character>().ToListAsync();
            return result;

        }
        //#endregion Character


        //Monster
        #region Monster


        public async Task<bool> InsertUpdateAsync_Monster(Monster data)
        {

            //// Check to see if the item exist
            //var oldData = await GetAsync_Monster(data.Id);
            //if (oldData == null)
            //{
            //    _monsterDataset.Add(data);
            //    return true;
            //}

            //// Compare it, if different update in the DB
            //var UpdateResult = await UpdateAsync_Monster(data);
            //if (UpdateResult)
            //{
            //    await AddAsync_Monster(data);
            //    return true;
            //}

            return false;
        }
        public async Task<bool> AddAsync_Monster(Monster data)
        {
        var result = await App.Database.InsertAsync(data);
        if (result == 1)
        {
            return true;
        }

        return false;

    }

    public async Task<bool> UpdateAsync_Monster(Monster data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;

    }

    public async Task<bool> DeleteAsync_Monster(Monster data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;

         }

        public async Task<Monster> GetAsync_Monster(string id)
        {
            var result = await App.Database.GetAsync<Monster>(id);
            return result;
        }

        public async Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Monster>().ToListAsync();
            return result;

        }

        #endregion Monster



    }
}

