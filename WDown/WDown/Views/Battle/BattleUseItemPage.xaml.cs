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
        public BattleUseItemPage(BattleViewModel _viewModel)
        {
            InitializeComponent();

        }
    }
}