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
    public partial class BattleItemPool : ContentPage
    {

        //List<Item> items = new List<Item>();

        Item selectedItem = null;

        private BattleViewModel _viewModel;

        // Initialize page and populate buttons
        public BattleItemPool(BattleViewModel viewModel)
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

        public void ShowItemLocation()
        {
            var currChar = _viewModel.BattleEngine.PlayerCurrent;
            ItemHead.Text = currChar.Head;
            Debug.WriteLine("Head string: " + ItemHead.Text);
            ItemHeadImage.Source = _viewModel.BattleEngine.PlayerCurrent.ImageURI;
            //ItemNecklass.Text = _viewModel.BattleEngine.PlayerCurrent.RemainingHP.ToString();
            //ItemPrimaryHand.Text = _viewModel.BattleEngine.PlayerCurrent.TotalHP.ToString();
            /*CPAttack.Text = _viewModel.BattleEngine.PlayerCurrent.Attack.ToString();
            CPDefense.Text = _viewModel.BattleEngine.PlayerCurrent.Defense.ToString();
            CPSpeed.Text = _viewModel.BattleEngine.PlayerCurrent.Speed.ToString();
            */
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

            if (selectedItem == null)
            {
                await Navigation.PopModalAsync();
            }

            //else
            //vonsume item
            //for now, all the consumables will jus trestore you to full health
            int value = selectedItem.Value;
            AttributeEnum modifiedAttr = selectedItem.Attribute;

            if (modifiedAttr == AttributeEnum.CurrentHealth)
            {
                _viewModel.BattleEngine.PlayerCurrent.RemainingHP = _viewModel.BattleEngine.PlayerCurrent.TotalHP;
                //also find current character in the character list and update it
                foreach (Models.Character character in _viewModel.BattleEngine.CharacterList)
                {
                    if (character.Name == _viewModel.BattleEngine.PlayerCurrent.Name)
                    {
                        character.CharacterAttribute.CurrentHealth = character.CharacterAttribute.MaxHealth;
                        Debug.WriteLine("Character has been healed to full health");
                    }
                }
            }

            await Navigation.PopModalAsync();



        }

        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args == null)
            {
                return;

            }

            //set selectedItem to currently selected item on list
            var data = args.SelectedItem as WDown.Models.Item;
            selectedItem = data;



            ItemDescription.Text = String.Format("{0}", selectedItem.Description);
            ItemEffectsLabel.Text = String.Format("This item affects {0} with value {1}", selectedItem.Attribute.ToString(), selectedItem.Value.ToString());

            SelectedItemImage.Source = selectedItem.ImageURI;

        }


    }
}