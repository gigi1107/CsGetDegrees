using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using WDown.Services;
using WDown.Models;

namespace WDown.ViewModels
{
    // Base View Model
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region RefactorLater
        // Two database: Mock is for Testing
        private IDataStore DataStoreMock => DependencyService.Get<IDataStore>() ?? MockDataStore.Instance;
        private IDataStore DataStoreSql => DependencyService.Get<IDataStore>() ?? SQLDataStore.Instance;

        public IDataStore DataStore;

        // Default using Mock
        public BaseViewModel()
        {
            SetDataStore(DataStoreEnum.Mock);
        }

        // Populate based on datastore type chosen
        public void SetDataStore(DataStoreEnum data)
        {
            switch (data)
            {
                case DataStoreEnum.Mock:
                    DataStore = DataStoreMock;
                    break;

                case DataStoreEnum.SQL:
                case DataStoreEnum.Unknown:
                default:
                    DataStore = DataStoreSql;
                    break;
            }
        }


        #endregion

        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        // Notify upon event changed
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
