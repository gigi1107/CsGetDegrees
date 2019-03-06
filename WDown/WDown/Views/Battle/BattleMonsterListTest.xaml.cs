using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDown.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WDown.Models;
using WDown.GameEngine;
namespace WDown.Views.Battle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleMonsterListTest : ContentPage
	{
        BattleMonsterListTest _myModalBattleMonsterListPage;

        private BattleEngine _battleEngine { get; set; }
        private BattleViewModel _battleViewModel;
        private BattleMonsterListViewModel _monsterViewModel;
        public BattleMonsterListTest()
		{
			InitializeComponent ();

            //_battleEngine = battleEngine;
            _monsterViewModel = BattleMonsterListViewModel.Instance;
            foreach (Models.Monster Monster in BattleViewModel.Instance.BattleEngine.MonsterList)
            {
                _monsterViewModel.FightingMonsters.Add(Monster);
            }
            BindingContext = _monsterViewModel;
        }


        // Handles when user click Next
        // Go to the Main Battle Page
        async void OnNextClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new WDown.Views.Battle.BattleMainPage());
        }
    }
}