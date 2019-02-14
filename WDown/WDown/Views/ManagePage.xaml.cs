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

        async void CharacterPageClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Character.CharacterPage());
        }

        async void MonsterPageClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Monster.MonsterPage());
        }


    }
}