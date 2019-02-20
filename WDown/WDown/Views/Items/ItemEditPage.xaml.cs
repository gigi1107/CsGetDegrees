using System;
using WDown.Models;
using WDown.Controllers;
using WDown.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WDown.Services;
namespace WDown.Views.Items
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemEditPage : ContentPage
	{
	    // ReSharper disable once NotAccessedField.Local
	    private ItemDetailViewModel _viewModel;

        // The data returned from the edit.
        public Item Data { get; set; }

        // The constructor takes a View Model
        // It needs to set the Picker values after doing the bindings.
        public ItemEditPage(ItemDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;

            viewModel.Title = "Edit " + viewModel.Title;

            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;

            // Checking which Wearable toggle is being used
            Data.Wearable = WearableSetting.IsToggled;

            
            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = Data.Location.ToString();
            AttributePicker.SelectedItem = Data.Attribute.ToString();

            // Turn off the LocationSetting Frame if toggled is off 
            // (because item is not wearable
            // All non-wearable items must have Location of "unknown"
            if (Data.Wearable == false)
            {
                LocationSettingsFrame.IsVisible = false;
                Data.Location = 0;
                LocationPicker.SelectedItem = 0;
            }

        }

        // Save on the Tool bar
        private async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in teh data box is empty, use the default one..
            if (string.IsNullOrEmpty(Data.ImageURI))
            {
                Data.ImageURI = ItemController.DefaultImageURI; 
            }

            MessagingCenter.Send(this, "EditData", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }

        // Cancel and go back a page in the navigation stack
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        //The stepper function for Range
        void Range_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            RangeValue.Text = String.Format("{0}", e.NewValue);
        }

        //The stepper function for Value
        void Value_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueValue.Text = String.Format("{0}", e.NewValue);
        }

        // The stepper function for Damage
        void Damage_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DamageValue.Text = String.Format("{0}", e.NewValue);
        }

        // This will allow user to toggle Wearable
        private void WearableSetting_OnToggled(object sender, ToggledEventArgs e)
        {
            Data.Wearable = e.Value;
            LocationSettingsFrame.IsVisible = (e.Value);
        }

    }
}