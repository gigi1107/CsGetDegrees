using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDown.ViewModels;
using WDown.Controllers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WDown.Views.Character
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterEditPage : ContentPage
    {
        private CharacterDetailViewModel _viewModel;

        public WDown.Models.Character Data { get; set; }

        public CharacterEditPage(CharacterDetailViewModel viewModel)

        {
            Data = viewModel.Data;

            viewModel.Title = "Edit " + viewModel.Title;

            InitializeComponent();

            BindingContext = _viewModel = viewModel;


        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(Data.ImageURI))
            {
                Data.ImageURI = CharacterController.DefaultImageURI;
            }

            MessagingCenter.Send(this, "EditData", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }

        // Cancel and go back a page in the navigation stack
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        // The stepper function for Attack change
        void Attack_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            AttackValue.Text = String.Format("{0}", e.NewValue);
        }

        // The stepper function for Speed change
        void Speed_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            SpeedValue.Text = String.Format("{0}", e.NewValue);
        }
        
        // The stepper function for Defense change
        void Defense_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            DefenseValue.Text = String.Format("{0}", e.NewValue);
        }


        // The stepper for Max Health change
        void MaxHealth_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            MaxHealthValue.Text = String.Format("{0}", e.NewValue);
        }

    }
}
