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
    public partial class TitlePage : ContentPage
    {
        public TitlePage()
        {
            InitializeComponent();

        }

        async void OnHistoryClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new HistoryPage());
        }

        async void OnCharacterClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Character.CharacterPage());
        }

        async void OnItemsClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ItemPage());
        }
    }
}
