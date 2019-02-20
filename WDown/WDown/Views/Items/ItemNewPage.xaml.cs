using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WDown.Controllers;
using WDown.Models;

namespace WDown.Views.Items
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemNewPage : ContentPage
    {
        public Item Data { get; set; }

        // Constructor for the page, will create a new black item that can tehn get updated
        public ItemNewPage()
        {
            InitializeComponent();

            Data = new Item
            {
                Name = "Item name",
                Description = "This is an item description.",
                Id = Guid.NewGuid().ToString(),
                Range = 0,
                Value = 1,
                ImageURI = ItemController.DefaultImageURI
            };
            BindingContext = this;

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
            } else if (Data.Wearable == true)
            {  // If the item is wearable, it cannot have "Unknown location"
                // If location is unknown, set location = head as default.
                ItemLocationEnum ChosenLocation = Data.Location;
                if (ChosenLocation == 0)
                {
                    Data.Location = (ItemLocationEnum)10;
                    BindingContext = this;
                    
                }
            }
        }

        /// Save on the Tool bar
        private async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in teh data box is empty, use the default one..
            if (string.IsNullOrEmpty(Data.ImageURI))
            {
                Data.ImageURI = ItemController.DefaultImageURI;
            }

            MessagingCenter.Send(this, "AddData", Data);
            await Navigation.PopAsync();
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