using WDown.ViewModels;
using WDown.Models;

namespace WDown.Services
{
    public static class MasterDataStore
    {
        // Holds which datastore to use.

        private static DataStoreEnum _dataStoreEnum = DataStoreEnum.Mock;

        // Returns which dtatstore to use
        public static DataStoreEnum GetDataStoreMockFlag()
        {
            return _dataStoreEnum;
        }

        // Switches the datastore values.
        // Loads the databases...
        public static void ToggleDataStore(DataStoreEnum dataStoreEnum)
        {
            switch (dataStoreEnum)
            {

                case DataStoreEnum.Mock:
                    _dataStoreEnum = DataStoreEnum.Mock;
                    ItemsViewModel.Instance.SetDataStore(DataStoreEnum.Mock);
                    MonstersViewModel.Instance.SetDataStore(DataStoreEnum.Mock);
                    CharactersViewModel.Instance.SetDataStore(DataStoreEnum.Mock);
                    // Implement Score

                    break;

                case DataStoreEnum.Sql:
                default:
                    _dataStoreEnum = DataStoreEnum.Sql;
                    ItemsViewModel.Instance.SetDataStore(DataStoreEnum.Sql);
                    MonstersViewModel.Instance.SetDataStore(DataStoreEnum.Sql);
                    CharactersViewModel.Instance.SetDataStore(DataStoreEnum.Sql);
                    // Implement Score
                    break;
            }

            // Load the Data
            ForceDataRestoreAll();
        }

        // Force all modes to load data...
        public static void ForceDataRestoreAll()
        {
            ItemsViewModel.Instance.ForceDataRefresh();
            MonstersViewModel.Instance.ForceDataRefresh();
            CharactersViewModel.Instance.ForceDataRefresh();
        }
    }
}
