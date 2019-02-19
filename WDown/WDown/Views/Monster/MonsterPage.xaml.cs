using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDown.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WDown.Models;
using WDown.Controllers;
using WDown.Services;
namespace WDown.Views.Monster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterPage : ContentPage
    {
        private MonstersViewModel _viewModel;

        // Constructor
        public MonsterPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = MonstersViewModel.Instance;
        }

        // Handles when user selects a particular monster 
        // Take user to the Detail View page of chosen monster
        private async void OnMonsterSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as WDown.Models.Monster;
            if (data == null)
                return;

            await Navigation.PushAsync(new MonsterDetailPage(new MonsterDetailViewModel(data)));

            // Manually deselect Monster.
            //MonsterListView.SelectedCharacter = null;
        }

        // Handles when user clicks Add
        // Takes user to New page so user can put in information for a new monster
        private async void AddMonster_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MonsterNewPage());
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
