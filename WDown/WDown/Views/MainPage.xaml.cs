using WDown.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WDown.Views.Items;

namespace WDown.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.About, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                    case (int)MenuItemType.Home:
                        MenuPages.Add(id, new NavigationPage(new TitlePage()));
                        break;
                    case (int)MenuItemType.Manage:
                        MenuPages.Add(id, new NavigationPage(new ManagePage()));
                        break;
                    case (int)MenuItemType.Items:
                        MenuPages.Add(id, new NavigationPage(new ItemPage()));
                        break;
                    case (int)MenuItemType.History:
                        MenuPages.Add(id, new NavigationPage(new HistoryPage()));
                        break;


                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}