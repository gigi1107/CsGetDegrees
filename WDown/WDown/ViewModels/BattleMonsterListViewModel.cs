using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using WDown.Models;
using WDown.Views;
using System.Linq;
using WDown.Controllers;
using WDown.GameEngine;
using WDown.Views.Battle;

namespace WDown.ViewModels
{
	public class BattleMonsterListViewModel : BaseViewModel
	{
        #region Singleton
        // Create one instance of view model
        private static BattleMonsterListViewModel _instance;

        public static BattleMonsterListViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new BattleMonsterListViewModel();
                return _instance;
            }
        }
        #endregion Singleton

        public ObservableCollection<Monster> FightingMonsters { get; set; }

        public BattleMonsterListViewModel()
        {
            FightingMonsters = new ObservableCollection<Monster>();
        }

    }
}