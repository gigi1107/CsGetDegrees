using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDown.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WDown.ViewModels;
// This page shows the list view of current characters
namespace WDown.Views.Character
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterPage : ContentPage
    {
        private CharactersViewModel _viewModel;

        public CharacterPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = CharactersViewModel.Instance;


        }

        private async void OnCharacterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as WDown.Models.Character;
            if (data == null)
                return;

            await Navigation.PushAsync(new CharacterDetailPage(new ViewModels.CharacterDetailViewModel(data)));

            CharacterList.SelectedItem = null;
        }

        // Handles when user wants to add a new character
        private async void AddCharacter_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharacterNewPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.RemoveAt(0);
            }

            InitializeComponent();

            if (_viewModel.Dataset.Count == 0)
            {
                _viewModel.LoadDataCommand.Execute(null);
            }
            else if (_viewModel.NeedsRefresh())
            {
                _viewModel.LoadDataCommand.Execute(null);
            }

            BindingContext = _viewModel;
        }
    }
}
