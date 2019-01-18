using System;

using Xamarin.Forms;

namespace week2.Views
{
    public class Title : ContentPage
    {
        public Title()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

