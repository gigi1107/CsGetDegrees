using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WDown.Services;
using WDown.Models;

namespace WDown.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            // Checking which Data Source toggle is currently being used
            // to populate Character and Monster pages
            SettingDataSource.IsToggled = (MasterDataStore.GetDataStoreMockFlag() == DataStoreEnum.Mock);
            SetDataSource(SettingDataSource.IsToggled);


        }

        // Handles when user clicks on toggle button
        private void Switch_OnToggled(object sender, ToggledEventArgs e)
        {
            // This will change out the DataStore to be the Mock Store if toggled on, or the SQL if off.
            SetDataSource(SettingDataSource.IsToggled);
        }

        // Decides to use MockDataStore or SQLDataStore
        private void SetDataSource(bool isMock)
        {
            var set = DataStoreEnum.SQL;

            if (isMock)
            {
                set = DataStoreEnum.Mock;
            }

            MasterDataStore.ToggleDataStore(set);
        }

        // Clear database, drop tables
        private async void ClearDatabase_Command(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Delete", "Sure you want to Delete All Data, and start over?", "Yes", "No");
            if (answer)
            {
                // Call to the SQL DataStore and have it clear the tables.
                SQLDataStore.Instance.InitializeDatabaseNewTables();
            }
        }

    }
}