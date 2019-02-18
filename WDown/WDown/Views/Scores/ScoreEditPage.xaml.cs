using System;

using WDown.Models;
using WDown.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WDown.Views.Scores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreEditPage : ContentPage
    {
        // ReSharper disable once NotAccessedField.Local
        private ScoreDetailViewModel _viewModel;

        public Score Data { get; set; }

        public ScoreEditPage(ScoreDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            viewModel.Title = "Edit " + viewModel.Title;

            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "EditData", Data);

            // removing the old ItemDetails page, 2 up counting this page
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            // Add a new items details page, with the new Item data on it
            await Navigation.PushAsync(new ScoreDetailPage(new ScoreDetailViewModel(Data)));

            // Last, remove this page
            Navigation.RemovePage(this);
        }

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