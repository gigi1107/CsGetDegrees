﻿using System;
using System.Collections.Generic;
using System.Text;

namespace week2.Models
{
    public enum MenuItemType
    {
        About,
        Home,
        Characters,
        Items,
        History
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
