using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WDown.Controllers
{
    public class ItemController
    {
        private static ItemController _instance;

        public static ItemController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ItemController();
                }
                return _instance;
            }
        }

        public static string DefaultImageURI = "https://i.imgur.com/DeFwZPA.png";
    }
}
