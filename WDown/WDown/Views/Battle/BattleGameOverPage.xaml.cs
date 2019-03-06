using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDown.ViewModels;
using WDown.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WDown.Views.Battle
{
    // This page shows the Game Over screen and let user navigate to Score Page
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleGameOverPage : ContentPage
	{
		public BattleGameOverPage ()
		{
			InitializeComponent ();
		}

        // Handle when user click next
        // Let user view Score Detail
        async void OnNextClicked(object sender, EventArgs args)
        {
            var myScoreObject = BattleViewModel.Instance.BattleEngine.BattleScore;

            await Navigation.PushModalAsync(new WDown.Views.Scores.ScoreDetailPage(new ScoreDetailViewModel(myScoreObject)));
        }

        /// <summary>
        ///  Don't need the page again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnDisappearing(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }


    }
}