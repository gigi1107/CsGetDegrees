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
        //create a list of Items to initialize the data
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

            //await AddAsync_Character(new Character
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = "Hazel",
            //    Description = "Hazel-rah, the leader of Watership Down warren."
            //});

            //await AddAsync_Character(new Character
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = "BigWit",
            //    Description = "The strongest defended of the warren, with a passion of fire."
            //});

            //await AddAsync_Monster(new Monster
            //{
            //    Id = Guid.NewGuid().ToString(),
            //    Name = "General Woundwort",
            //    Description = "The tyranny leader of Efrafa Warren."
            //});
             
            // Implement Characters
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL First Character", Description = "This is an Character description.", Level = 1 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL Second Character", Description = "This is an Character description.", Level = 1 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL Third Character", Description = "This is an Character description.", Level = 2 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL Fourth Character", Description = "This is an Character description.", Level = 2 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL Fifth Character", Description = "This is an Character description.", Level = 3 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL Sixth Character", Description = "This is an Character description.", Level = 3 });

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

        #region Character
        // Character
        // Conver to BaseCharacter and then add it
        public async Task<bool> AddAsync_Character(Character data)
        {
            // Convert Character to CharacterBase before saving to Database
            var dataBase = new BaseCharacter(data);

            var result = await App.Database.InsertAsync(dataBase);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> InsertUpdateAsync_Character(Character data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Character(data.Id);
            if (oldData == null)
            {
                await AddAsync_Character(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Character(data);
            if (UpdateResult)
            {
                await AddAsync_Character(data);
                return true;
            }

            return false;
        }

        // Convert to BaseCharacter and then update it
        public async Task<bool> UpdateAsync_Character(Character data)
        {
            // Convert Character to CharacterBase before saving to Database
            var dataBase = new BaseCharacter(data);

            var result = await App.Database.UpdateAsync(dataBase);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        // Pass in the character and convert to Character to then delete it
        public async Task<bool> DeleteAsync_Character(Character data)
        {
            // Convert Character to CharacterBase before saving to Database
            var dataBase = new BaseCharacter(data);

            var result = await App.Database.DeleteAsync(dataBase);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        // Get the Character Base, and Load it back as Character
        public async Task<Character> GetAsync_Character(string id)
        {
            var tempResult = await App.Database.GetAsync<BaseCharacter>(id);

            var result = new Character(tempResult);

            return result;
        }

        // Load each character as the base character, 
        // Then then convert the list to characters to push up to the view model
        public async Task<IEnumerable<Character>> GetAllAsync_Character(bool forceRefresh = false)
        {
            var tempResult = await App.Database.Table<BaseCharacter>().ToListAsync();

            var result = new List<Character>();
            foreach (var item in tempResult)
            {
                result.Add(new Character(item));
            }

            return result;
        }

        #endregion Character


        #region Monster
        //Monster
        // Conver to BaseMonster and then add it
        public async Task<bool> AddAsync_Monster(Monster data)
        {
            // Convert Monster to MonsterBase before saving to Database
            var dataBase = new BaseMonster(data);

            var result = await App.Database.InsertAsync(dataBase);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> InsertUpdateAsync_Monster(Monster data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Monster(data.Id);
            if (oldData == null)
            {
                await AddAsync_Monster(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Monster(data);
            if (UpdateResult)
            {
                await AddAsync_Monster(data);
                return true;
            }

            return false;
        }

        // Convert to BaseMonster and then update it
        public async Task<bool> UpdateAsync_Monster(Monster data)
        {
            // Convert Monster to MonsterBase before saving to Database
            var dataBase = new BaseMonster(data);

            var result = await App.Database.UpdateAsync(dataBase);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        // Pass in the Monster and convert to Monster to then delete it
        public async Task<bool> DeleteAsync_Monster(Monster data)
        {
            // Convert Monster to MonsterBase before saving to Database
            var dataBase = new BaseMonster(data);

            var result = await App.Database.DeleteAsync(dataBase);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        // Get the Monster Base, and Load it back as Monster
        public async Task<Monster> GetAsync_Monster(string id)
        {
            var tempResult = await App.Database.GetAsync<BaseMonster>(id);

            var result = new Monster(tempResult);

            return result;
        }

        // Load each Monster as the base Monster, 
        // Then then convert the list to Monsters to push up to the view model
        public async Task<IEnumerable<Monster>> GetAllAsync_Monster(bool forceRefresh = false)
        {
            var tempResult = await App.Database.Table<BaseMonster>().ToListAsync();

            var result = new List<Monster>();
            foreach (var item in tempResult)
            {
                result.Add(new Monster(item));
            }

            return result;
        }

        #endregion Monster



    }
}

