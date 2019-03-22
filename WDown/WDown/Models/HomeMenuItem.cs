using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace WDown.Models
{

    // Enum menu for About, Manage, and Game
    public enum MenuItemType
    {
        About,
  
        Manage,

        Game

    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
