using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDown.Views.Items;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace WDown.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ManagePage : ContentPage

    { 
		public ManagePage ()
		{
			InitializeComponent ();

		}

        // Take user to Character Page
        async void CharacterPageClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Character.CharacterPage());
        }

        // Take user to Monster Page
        async void MonsterPageClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Monster.MonsterPage());
        }

        // Take user to Item Page
        async void ItemPageClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Items.ItemPage());
        }


    }
}