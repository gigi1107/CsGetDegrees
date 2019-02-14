using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WDown.Services;
using WDown.ViewModels;
using WDown.Models;
using Xamarin.Forms;

namespace WDown.Controllers
{
    public class CharacterController
    {
        private static CharacterController _instance;

        public static CharacterController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CharacterController();
                }
                return _instance;
            }
        }

        public static string DefaultImageURI = "https://i.imgur.com/lcTjFJ1.png";
    }
}

