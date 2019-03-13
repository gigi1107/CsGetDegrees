using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WDown.Views;
using WDown.Controllers;
using SQLite;
using WDown.Services;
using WDown.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace WDown
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();


            MainPage = new MainPage();
            // Load The Mock Datastore by default
            MasterDataStore.ToggleDataStore(DataStoreEnum.Mock);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        static SQLiteAsyncConnection _database;

        public static SQLiteAsyncConnection Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new SQLiteAsyncConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath("DatabaseKoenig1.db3"));
                }
                return _database;
            }
        }
    }
}
