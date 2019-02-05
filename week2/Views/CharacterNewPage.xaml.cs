using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using week2.Controllers;
using week2.Models;

namespace week2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterNewPage : ContentPage
    {
        public Character Data { get; set; }

        // Constructor for the page, will create a new black item that can tehn get updated
        public CharacterNewPage()
        {
            InitializeComponent();

            Data = new Character
            {
                Name = "Character name",
                Age = 1,
                Description = "This is an item description.",
                Id = Guid.NewGuid().ToString(),
                ImageURI = "rabbit.png"
               
            
        };

            BindingContext = this;
           
        }

        // Respond to the Save click
        // Send the add message to so it gets added...
        private async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in teh data box is empty, use the default one..
            if (string.IsNullOrEmpty(Data.ImageURI))
            {
                Data.ImageURI = CharacterController.DefaultImageURI;
            }

            MessagingCenter.Send(this, "AddData", Data);
            await Navigation.PopAsync();
        }

        // Cancel and go back a page in the navigation stack
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        // The stepper function for Range
        void Age_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            AgeValue.Text = String.Format("{0}", e.NewValue);
        }

      

    }
}
