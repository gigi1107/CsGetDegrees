using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDown.Controllers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WDown.Views.Scores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]



    public partial class ScoresNewPage : ContentPage
    {
        public Models.Score Data { get; set; }
        public ScoresNewPage()
        {
            InitializeComponent();

            Data = new Models.Score
            {
                Name = "New Score",
                Description = "This is a new Score",
                Id = Guid.NewGuid().ToString(),
                ImageURI = "https://i.imgur.com/DeFwZPA.png"
            };
            BindingContext = this;

        }
        private async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in teh data box is empty, use the default one..
            if (string.IsNullOrEmpty(Data.ImageURI))
            {
                Data.ImageURI = "https://i.imgur.com/DeFwZPA.png";
            }

            MessagingCenter.Send(this, "AddData", Data);
            await Navigation.PopAsync();
        }

        // Cancel and go back a page in the navigation stack
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


        // This function handles stepper value changed for Attack
        private void Score_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            ScoreValue.Text = String.Format("{0}", e.NewValue);
        }


        // This function handles stepper value changed for Speed
        private void Turn_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            TurnCountValue.Text = String.Format("{0}", e.NewValue);
        }


        // This function handles stepper value changed for Defense
        private void RoundCount_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            RoundValue.Text = String.Format("{0}", e.NewValue);
        }

        private void MonstersSlainNumber_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            MonstersKilledNumber.Text = String.Format("{0}", e.NewValue);
        }

        private void XP_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            XPGained.Text = String.Format("{0}", e.NewValue);
        }

        private void BattleNumber_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            BattleNum.Text = String.Format("{0}", e.NewValue);
        }

       
    }
}
