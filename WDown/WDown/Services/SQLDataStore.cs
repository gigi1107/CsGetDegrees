using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WDown.Models;
using WDown.ViewModels;
using SQLite;

namespace WDown.Services
{
    // This page allows the app to communicate with the SQL database
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
            // Create new db tables
            CreateTables();
          
        }
        // Initialize tables inside database
        public void InitializeDatabaseNewTables()
        {

            // Drop all tables and make new ones
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
            // Character
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL First Character", Description = "This is an Character description.", Level = 1 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL Second Character", Description = "This is an Character description.", Level = 1 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL Third Character", Description = "This is an Character description.", Level = 2 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL Fourth Character", Description = "This is an Character description.", Level = 2 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL Fifth Character", Description = "This is an Character description.", Level = 3 });
            await AddAsync_Character(new Character { Id = Guid.NewGuid().ToString(), Name = "SQL Sixth Character", Description = "This is an Character description.", Level = 3 });

            // Monster
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQL First Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQL Second Monster", Description = "This is an Monster description." });
            await AddAsync_Monster(new Monster { Id = Guid.NewGuid().ToString(), Name = "SQL Third Monster", Description = "This is an Monster description." });
        }

        #region Item
        // Item
        public async Task<bool> InsertUpdateAsync_Item(Item data)
        {

            // Implement
            var _count = App.Database.Table<Item>().Where(i => i.Id == data.Id).CountAsync().Result;
            if (_count == 0)
            {
                return await AddAsync_Item(data);
            }
            else
            {
                return await UpdateAsync_Item(data);
            }

        }   
        // Add a new item to DB
        public async Task<bool> AddAsync_Item(Item data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;

        }
        // Edit an item from DB
        public async Task<bool> UpdateAsync_Item(Item data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;

    }
        // Delete an item from DB
        public async Task<bool> DeleteAsync_Item(Item data)
        {
            var result = await App.Database.DeleteAsync(data);
            if (result == 1)
            {
                return true;
            }

        return false;

    }
        // Get information of an item
        public async Task<Item> GetAsync_Item(string id)
        {
            var result = await App.Database.GetAsync<Item>(id);
            return result;
        }


        // Get list of current items
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

        // Insert update a character to db
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

        // Insert update a monster
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

        #region Score
        // Score

            // Add a new score to the DB
        public async Task<bool> AddAsync_Score(Score data)
        {
            var result = await App.Database.InsertAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        // Update a score's information in DB
        public async Task<bool> UpdateAsync_Score(Score data)
        {
            var result = await App.Database.UpdateAsync(data);
            if (result == 1)
            {
                return true;
            }

            return false;
        }

        // Delete a score in DB
        public async Task<bool> DeleteAsync_Score(Score data)
        {
           
                var result = await App.Database.DeleteAsync(data);
                if (result == 1)
                {
                    return true;
                }

                return false;

         }

        // Get information of a score from DB
            public async Task<Score> GetAsync_Score(string id)
        {
            var result = await App.Database.GetAsync<Score>(id);
            return result;

        }

        // Get list and information of current scores
        public async Task<IEnumerable<Score>> GetAllAsync_Score(bool forceRefresh = false)
        {
            var result = await App.Database.Table<Score>().ToListAsync();
            return result;
        }

        // Insert update a score
        public async Task<bool> InsertUpdateAsync_Score(Score data)
        {

            // Check to see if the item exist
            var oldData = await GetAsync_Score(data.Id);
            if (oldData == null)
            {
                await AddAsync_Score(data);
                return true;
            }

            // Compare it, if different update in the DB
            var UpdateResult = await UpdateAsync_Score(data);
            if (UpdateResult)
            {
                await AddAsync_Score(data);
                return true;
            }

            return false;
        }
        #endregion Score



    }
}

