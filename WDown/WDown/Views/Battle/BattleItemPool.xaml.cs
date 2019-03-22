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

// This page populates the item pool page
// which allows characters to drop their item to 
// get a new one if desire
namespace WDown.Views.Battle
{
    // This page populates the Use Item view for the Battle Main Page
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BattleItemPool : ContentPage
    {

        // Currently selected items
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

        // This binds the item at the current location
        // or indicates if the item is empty
       
        public void ShowItemLocation()
        {

            // Getting the Character by ID
            var charName = _viewModel.BattleEngine.PlayerCurrent.Name;
            var currChar = _viewModel.GetByName(charName);

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
           
            var charName = _viewModel.BattleEngine.PlayerCurrent.Name;
            var currChar = _viewModel.GetByName(charName);
            // Get Item clicked
            int value = selectedItem.Value;
            AttributeEnum modifiedAttr = selectedItem.Attribute;
            ItemLocationEnum modifiedLocation = selectedItem.Location;
            string itemID = selectedItem.Id;
            
            // Equip item into that location, get back an item previously there
            // check if returned item is null
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

        // This function handles when button Save is clicked
        // An item must be chosen before clicking Save. If not, user 
        // has to click Cancel.

        async void SaveButtonClicked(object sender, EventArgs e)
        {

           // Pop Modal Page

            if (selectedItem == null)
            {
                await Navigation.PopModalAsync();
            }

           

            await Navigation.PopModalAsync();



        }
        // Handle when user clicks Cancel button
        async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        
        // When item is selected, populates the additional bar to show additional details 
        // and also help with EquipClicked() 
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            // Check if null
            if (args == null)
            {
                return;

            }

            //set selectedItem to currently selected item on list
            var data = args.SelectedItem as WDown.Models.Item;
            selectedItem = data;


            // Populates the front end with data
            ItemDescription.Text = String.Format("{0}", selectedItem.Description);
            ItemEffectsLabel.Text = String.Format("This item affects {0} with value {1}", selectedItem.Attribute.ToString(), selectedItem.Value.ToString());

            SelectedItemImage.Source = selectedItem.ImageURI;

        }


    }
}