using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDown.Controllers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WDown.Views.Monster
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterNewPage : ContentPage
    {
        public WDown.Models.Monster Data { get; set; }

        public MonsterNewPage()
        {
            InitializeComponent();

            Data = new WDown.Models.Monster
            {
                Name = "Unknown Monster",
                Description = "This is a monster description.",
                Id = Guid.NewGuid().ToString(),
                ImageURI = MonsterController.DefaultImageURI
            };

            BindingContext = this;
            //AttributePicker.SelectedItem = Data.Attribute.ToString();
        }
        private async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in teh data box is empty, use the default one..
            if (string.IsNullOrEmpty(Data.ImageURI))
            {
                Data.ImageURI = MonsterController.DefaultImageURI;
            }

            MessagingCenter.Send(this, "AddData", Data);
            await Navigation.PopAsync();
        }

        // Cancel and go back a page in the navigation stack
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        //The stepper function for Attack
        //void Attack_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        //{
        //    AttackValue.Text = String.Format("{0}", e.NewValue);
        //}

    }
}