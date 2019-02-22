using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using WDown.ViewModels;
using WDown.Models;
using System.Linq;
using WDown.Controllers;

namespace WDown.ViewModels
{
    public class ScoresViewModel : BaseViewModel
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static ScoresViewModel _instance;

        public static ScoresViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ScoresViewModel();
                }
                return _instance;
            }
        }

        public ObservableCollection<Score> Dataset { get; set; }
        public Command LoadDataCommand { get; set; }

        private bool _needsRefresh;

        public ScoresViewModel()
        {
            Title = "Score List";
            Dataset = new ObservableCollection<Score>();
            LoadDataCommand = new Command(async () => await ExecuteLoadDataCommand());

            MessagingCenter.Subscribe<WDown.Views.Scores.ScoreDeletePage, Score>(this, "DeleteData", async (obj, data) =>
            {
                Dataset.Remove(data);
                await DataStore.DeleteAsync_Score(data);
            });

            MessagingCenter.Subscribe<WDown.Views.Scores.ScoresNewPage, Score>(this, "AddData", async (obj, data) =>
            {
                Dataset.Add(data);
                await DataStore.AddAsync_Score(data);
            });

            MessagingCenter.Subscribe<WDown.Views.Scores.ScoreEditPage, Score>(this, "EditData", async (obj, data) =>
            {

                // Find the Item, then update it
                var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
                if (myData == null)
                {
                    return;
                }

                myData.Update(data);
                await DataStore.UpdateAsync_Score(data);

                _needsRefresh = true;

            });
        }

        public bool NeedsRefresh()
        {
            if (_needsRefresh)
            {
                _needsRefresh = false;
                return true;
            }
            return false;
        }

        // Sets the need to refresh
        public void SetNeedsRefresh(bool value)
        {
            _needsRefresh = value;
        }

        private async Task ExecuteLoadDataCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Dataset.Clear();
                var dataset = await DataStore.GetAllAsync_Score(true);

                // Example of how to sort the database output using a linq query.
                //Sort the list
                dataset = dataset
                    .OrderBy(a => a.Name)
                    .ThenBy(a => a.BattleNumber)
                    .ThenBy(a => a.GameDate)
                    .ThenByDescending(a => a.ScoreTotal)
                    .ToList();

                // Then load the data structure
                foreach (var data in dataset)
                {
                    Dataset.Add(data);
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            finally
            {
                IsBusy = false;
            }
        }
        // Force data to refresh
        public void ForceDataRefresh()
        {
            // Reset
            var canExecute = LoadDataCommand.CanExecute(null);
            LoadDataCommand.Execute(null);
        }
        #region DataOperations

        // Add a new Score
        public async Task<bool> AddAsync(Score data)
        {
            Dataset.Add(data);
            var myReturn = await DataStore.AddAsync_Score(data);
            return myReturn;
        }
        // Delete a current Item
        public async Task<bool> DeleteAsync(Score data)
        {
            Dataset.Remove(data);
            var myReturn = await DataStore.DeleteAsync_Score(data);
            return myReturn;
        }

        // Edit/update a current Score
        public async Task<bool> UpdateAsync(Score data)
        {
            // Find the Score, then update it
            var myData = Dataset.FirstOrDefault(arg => arg.Id == data.Id);
            if (myData == null)
            {
                return false;
            }

            myData.Update(data);
            await DataStore.UpdateAsync_Score(myData);

            _needsRefresh = true;

            return true;
        }

        // Call to database to ensure most recent
        public async Task<Score> GetAsync(string id)
        {
            var myData = await DataStore.GetAsync_Score(id);
            return myData;
        }

        // Having this at the ViewModel, because it has the DataStore
        // That allows the feature to work for both SQL and the MOCk datastores...
        public async Task<bool> InsertUpdateAsync(Score data)
        {
            var myReturn = await DataStore.InsertUpdateAsync_Score(data);
            return myReturn;
        }

        public Score CheckIfScoreExists(Score data)
        {
            // This will walk the score and find if there is one that is the same.
            // If so, it returns the score

            var myList = Dataset.Where(a =>
                                        a.Name == data.Name &&
                                        a.BattleNumber == data.BattleNumber &&
                                        a.ScoreTotal == data.ScoreTotal &&
                                        a.GameDate == data.GameDate &&
                                        a.AutoBattle == data.AutoBattle &&
                                        a.TurnCount == data.TurnCount &&
                                        a.RoundCount == data.RoundCount)
                                        .FirstOrDefault();

            if (myList == null)
            {
                // it's not a match, return false;
                return null;
            }

            return myList;
        }

        #endregion DataOperations

        #region ScoreConversion

        // Takes an item string ID and looks it up and returns the item
        // This is because the Items on a character are stores as strings of the GUID.  That way it can be saved to the DB.
        public Score GetScore(string ScoreID)
        {
            if (string.IsNullOrEmpty(ScoreID))
            {
                return null;
            }

            Score myData = DataStore.GetAsync_Score(ScoreID).GetAwaiter().GetResult();
            if (myData == null)
            {
                return null;
            }

            return myData;
        }

        #endregion ScoreConversion

        
    }



}