using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WDown.Services;
using WDown.ViewModels;
using WDown.Models;
using Xamarin.Forms;

// Character controller
// Specify the default URL for the image for Character class
namespace WDown.Controllers
{
    public class CharacterController
    {
        private static CharacterController _instance;
        // Instantiate
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
        // Image URI 
        public static string DefaultImageURI = "https://i.imgur.com/lcTjFJ1.png";
    }
}

