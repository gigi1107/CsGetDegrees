using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDown.ViewModels;
using WDown.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WDown.Views.Battle
{
    // This page populates the Use Item view for the Battle Main Page
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BattleUseItemPage : ContentPage
    {
        public List<WDown.Models.Item> Datalist = new List<WDown.Models.Item>();

        private BattleViewModel _viewModel;
        // Initialize page and populate buttons
        public BattleUseItemPage(BattleViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;

            CharacterImage.Source = BattleViewModel.Instance.currentPlayerURI;
            CharacterAttack.Text = String.Format("{0}", BattleViewModel.Instance.currentPlayerAttack);
            CharacterDefense.Text = String.Format("{0}", BattleViewModel.Instance.currentPlayerDefense);
            CharacterSpeed.Text = String.Format("{0}", BattleViewModel.Instance.currentPlayerSpeed);
            CharacterName.Text = String.Format("{0}", BattleViewModel.Instance.currentPlayerName);
            CharacterCurrentHealth.Text = String.Format("{0}", BattleViewModel.Instance.currentPlayerHPTotal);
        }

        public async void SaveButtonClicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new ItemLocationSelectPage());
        }

        public async void CancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Item item))
                return;

            ItemDescription.Text = String.Format("{0}", item.Description);
            ItemAffectsLabel.Text = String.Format("This item affects {0} with value {1}", item.Attribute.ToString(), item.Value.ToString());

            ItemImage.Source = item.ImageURI;
        }
    }
}