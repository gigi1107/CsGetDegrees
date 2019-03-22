using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDown.ViewModels;
using WDown.Models;
using WDown.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WDown.Views.Monster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterEditPage : ContentPage
    {
        // This page allows users to edit the values of a monter
        private MonsterDetailViewModel _viewModel;

        public WDown.Models.Monster Data { get; set; }


        public MonsterEditPage(MonsterDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
           // Items = Services.MockDataStore.Instance.GetItemList();


            viewModel.Title = "Edit " + viewModel.Title;

            InitializeComponent();


            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
           
        }

        // This function saves the information that the user just put in 
        public async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "EditData", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new MonsterDetailPage(new MonsterDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }

        // This function handles when users click Cancel button and discard the new input values
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        // This function handles stepper value changed for Attack
        private void Attack_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            AttackValue.Text = String.Format("{0}", e.NewValue);
        }


        // This function handles stepper value changed for Speed
        private void Speed_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            SpeedValue.Text = String.Format("{0}", e.NewValue);
        }


        // This function handles stepper value changed for Defense
        private void Defense_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DefenseValue.Text = String.Format("{0}", e.NewValue);
        }


        // This function handles stepper value changed for DropRate
        private void DropRate_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DropRateValue.Text = String.Format("{0}", e.NewValue);
        }

        // This function handles drop-down menu select value for Item Drop
        private void ItemDrop_OnItemSelected(object sender, ValueChangedEventArgs e)
        {
            ItemDrop.SelectedItem = String.Format("{0}", e.NewValue);
        }
    }
}
