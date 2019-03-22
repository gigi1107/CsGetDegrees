using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WDown.Controllers
{
    // Monster Controller
    // Set the default URI for image for Monster class
    public class MonsterController
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static MonsterController _instance;

        public static MonsterController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MonsterController();
                }
                return _instance;
            }
        }

        // Return the Default Image URI for the Local Image for a Monster
        public static string DefaultImageURI = "https://i.imgur.com/OnGOYw9.png";
    }
}

