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
            ShowItemLocation();
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

            // Getting the Character by ID
            var charID = _viewModel.BattleEngine.PlayerCurrent.Guid;
            var currChar = _viewModel.Get(charID);

            List<Item> itemByLocation = new List<Item>();
            // Getting Item from each Location in Character
            
            // Head slot
            Item currItem = currChar.GetItemByLocation(ItemLocationEnum.Head);
            if (currItem != null)
            {
                itemByLocation.Add(currItem);
                ItemHead.Text = currItem.Name;
                ItemHeadImage.Source = currItem.ImageURI;
            }
            else ItemHead.Text = "Empty";


            // Necklass slot
            currItem = currChar.GetItemByLocation(ItemLocationEnum.Necklass);
            if (currItem != null)
            {
                itemByLocation.Add(currItem);
                ItemNecklass.Text = currItem.Name;
                ItemNecklassImage.Source = currItem.ImageURI;
            }
            else ItemNecklass.Text = "Empty";
                

            // PrimaryHand slot
            currItem = currChar.GetItemByLocation(ItemLocationEnum.PrimaryHand);
            if (currItem != null)
            {
                itemByLocation.Add(currItem);
                ItemPrimaryHand.Text = currItem.Name;
                ItemPrimaryHandImage.Source = currItem.ImageURI;
            }
            else ItemPrimaryHand.Text = "Empty";

            // OffHand slot

            currItem = currChar.GetItemByLocation(ItemLocationEnum.OffHand);
            if (currItem != null)
            {
                itemByLocation.Add(currItem);
                ItemOffHand.Text = currItem.Name;
                ItemOffHandImage.Source = currItem.ImageURI;
            }
            else ItemOffHand.Text = "Empty";

            // LeftFinger slot
            currItem = currChar.GetItemByLocation(ItemLocationEnum.LeftFinger);
            if (currItem != null)
            {
                itemByLocation.Add(currItem);
                ItemLeftFinger.Text = currItem.Name;
                ItemLeftFingerImage.Source = currItem.ImageURI;
            }
            else ItemLeftFinger.Text = "Empty";

            // Right Finger slot
            currItem = currChar.GetItemByLocation(ItemLocationEnum.RightFinger);
            if (currItem != null)
            {
                itemByLocation.Add(currItem);
                ItemRightFinger.Text = currItem.Name;
                ItemRightFingerImage.Source = currItem.ImageURI;
            }
            else ItemRightFinger.Text = "Empty";

            // Feet slot
            currItem = currChar.GetItemByLocation(ItemLocationEnum.Feet);
            if (currItem != null)
            {
                itemByLocation.Add(currItem);
                ItemFeet.Text = currItem.Name;
                ItemFeetImage.Source = currItem.ImageURI;
            }
            else ItemFeet.Text = "Empty";

            // Another way to populate the XAML front-end dynamically
            AvailableItemLocationListView.ItemsSource = itemByLocation;


        }

        // Using ItemSlotsFormatOutput() from Character.cs
        // to see the list of currently equipped item
        async void GetItemSlotsClicked(object sender, EventArgs e)
        {
            // Getting the Character by ID
            Debug.WriteLine("Printing currently worn items...");
            var charID = _viewModel.BattleEngine.PlayerCurrent.Guid;
            var currChar = _viewModel.Get(charID);
            string charName = currChar.Name;
            Debug.WriteLine("Character's name: " + charName);

            var itemSlot = currChar.ItemSlotsFormatOutput();
            Debug.WriteLine(itemSlot);
        }

        // This method handles when user click equip
        // Equip ALWAYS equip the selected item.
        // If there is already an item at the same slot, the currently 
        // selected item will be added to the location, and the old item 
        // will be dropped back to the Item Pool
        async void EquipClicked(object sender, EventArgs e)
        {
            if (e == null)
            {
                return;

            }
            Debug.WriteLine("You are equipping item....");
            // Get Character
            var charID = _viewModel.BattleEngine.PlayerCurrent.Guid;
            var currChar = _viewModel.Get(charID);
            // Get Item clicked
            int value = selectedItem.Value;
            AttributeEnum modifiedAttr = selectedItem.Attribute;
            ItemLocationEnum modifiedLocation = selectedItem.Location;
            string itemID = selectedItem.Id;
            
            // Add item into that location
            var returnedItem = currChar.AddItem(modifiedLocation, itemID);

            // Printing debug output
            Debug.WriteLine("Trying to equip item " + selectedItem.Name + " at " + modifiedLocation);
            if (returnedItem != null)
            {
                Debug.WriteLine("Removed item: " + returnedItem.Name + " at " + returnedItem.Location);
                _viewModel.BattleEngine.ItemPool.Add(returnedItem);
            } else
            {
                Debug.WriteLine("There was no item there before equipping.");
            }

            // Remove equipped item from the pool
            var recentlyEquippedItem = currChar.GetItemByLocation(modifiedLocation);
            _viewModel.BattleEngine.ItemPool.Remove(recentlyEquippedItem);
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