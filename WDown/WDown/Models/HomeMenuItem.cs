using System;
using System.Collections.Generic;
using System.Text;

namespace WDown.Models
{

    public enum MenuItemType
    {
        About,
        Home,
        Manage,
        Items,
        History
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
