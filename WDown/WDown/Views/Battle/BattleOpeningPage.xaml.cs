using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WDown.Views.Battle
{
    // This page populates the Battle Opening Page
    // This allows users to move on and select characters
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleOpeningPage : ContentPage
	{
		public BattleOpeningPage ()
		{
			InitializeComponent ();
		}

        // This starts the Character Select Page    
        private async void Character_Select_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WDown.Views.Battle.BattleCharacterSelectPage());
        }
    }
}