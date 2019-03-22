

using WDown.Models;
using WDown.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WDown.Views.Battle
{
    // This page let user selects 6 characters to start the battle 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BattleCharacterSelectPage : ContentPage
    {
        // Holds a BattleViewModel object
        private BattleViewModel _viewModel;

        // Start page
        public BattleCharacterSelectPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = BattleViewModel.Instance;

            BattleViewModel.Instance.ClearCharacterLists();
        


            // Start with Next button disabled
            NextButton.IsEnabled = false;
        }

        // Close this page
        async void OnNextClicked(object sender, EventArgs args)
        {
           
            if(_viewModel.BattleEngine.BattleScore.RoundCount > 1)
            {
                await Navigation.PopModalAsync();
                return;
            }

           
            // Jump to Main Battle Page
            await Navigation.PushAsync(new BattleMonsterListPage());

            // Last, remove this page
            Navigation.RemovePage(this);
        }

        // Shows currently available Characters
        private async void OnAvailableCharacterItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as WDown.Models.Character;
            if (data == null)
            {
                return;
            }

            // Manually deselect item.
            AvailableCharactersListView.SelectedItem = null;

          

            var currentCount = _viewModel.SelectedCharacters.Count();
            // Don't add more than the party max
            if (currentCount < GameGlobals.MaxNumberPartyPlayers)
            {

                
                _viewModel.SelectedListAdd(data);
                //MessagingCenter.Send(this, "AddSelectedCharacter", data);
            }

            // refresh the count
            currentCount = _viewModel.SelectedCharacters.Count();

            // Set the Button to be enabled or disabled if no characters in the party
            NextButton.IsEnabled = true;
            if (currentCount == 0)
            {
                NextButton.IsEnabled = false;
            }

            PartyCountLabel.Text = currentCount.ToString();
        }

        // When user clicks on a Character, it shows in the selected tab
        private async void OnSelectedCharacterItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as WDown.Models.Character;
            if (data == null)
            {
                return;
            }

            // Manually deselect item.
            SelectedCharactersListView.SelectedItem = null;

            MessagingCenter.Send(this, "RemoveSelectedCharacter", data);

            // If no characters disable Next button
            var currentCount = _viewModel.SelectedCharacters.Count();
            if (currentCount == 0)
            {
                NextButton.IsEnabled = false;
            }

            PartyCountLabel.Text = currentCount.ToString();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;


            InitializeComponent();

            // Clear the Selected Ones, start over.
            _viewModel.SelectedCharacters.Clear();

            // If the Available Character List is empty fill it and then show it
            if (_viewModel.AvailableCharacters.Count == 0)
            {
                _viewModel.LoadDataCommand.Execute(null);
            }
            else if (_viewModel.NeedsRefresh())
            {
                _viewModel.LoadDataCommand.Execute(null);
            }

            BindingContext = _viewModel;

            PartyCountLabel.Text = _viewModel.SelectedCharacters.Count().ToString();

        }

    }
}