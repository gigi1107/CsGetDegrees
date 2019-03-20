using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WDown.ViewModels;
using WDown.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.SimpleAudioPlayer;
using System.IO;
using System.Reflection;

namespace WDown.Views.Battle
{
    // This page populates the Use Item view for the Battle Main Page
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BattleUseItemPage : ContentPage
    {

        //List<Item> items = new List<Item>();

        Item selectedItem = null;

        private BattleViewModel _viewModel;

        // Initialize page and populate buttons
        public BattleUseItemPage(BattleViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;

            ShowPlayerStats();
            //grab the itemslist from battle engine where the item is not wearable
           
        }


        public void ShowPlayerStats()
        {
            CPName.Text = _viewModel.BattleEngine.PlayerCurrent.Name;
            CPImage.Source = _viewModel.BattleEngine.PlayerCurrent.ImageURI;
            CPHPCurr.Text = _viewModel.BattleEngine.PlayerCurrent.RemainingHP.ToString();
            CPHPTotal.Text = _viewModel.BattleEngine.PlayerCurrent.TotalHP.ToString();
            CPAttack.Text = _viewModel.BattleEngine.PlayerCurrent.Attack.ToString();
            CPDefense.Text = _viewModel.BattleEngine.PlayerCurrent.Defense.ToString();
            CPSpeed.Text = _viewModel.BattleEngine.PlayerCurrent.Speed.ToString();

            AvailableItemListView.ItemsSource = _viewModel.BattleEngine.ItemPool;


            Debug.WriteLine("Now the Items list in the backend to compare: ");
            foreach (Item item in _viewModel.BattleEngine.ItemPool)
            {
                Debug.WriteLine(item.Name);
            }

        }


        async void SaveButtonClicked(object sender, EventArgs e)
        {

            //consume item
            //pop item off list
            //heal character or whatever
            //pop modal page

            if(selectedItem == null)
            {
               await Navigation.PopModalAsync();
            }


        }

        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

         async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Item item))
                return;

            var data = args.SelectedItem as WDown.Models.Item;
            selectedItem = data;

            //ItemDescription.Text = String.Format("{0}", item.Description);
            //ItemEffectsLabel.Text = String.Format("This item affects {0} with value {1}", item.Attribute.ToString(), item.Value.ToString());

            //SelectedItemImage.Source = selectedItem.ImageURI;

        }

        
    }
}