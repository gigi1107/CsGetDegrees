using System;
using week2.Services;
using week2.Models;
using week2.ViewModels;

using Xamarin.Forms;

namespace week2.Controllers
{
    public class CharacterController
    {
        private static CharacterController _instance;

        public static CharacterController Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new CharacterController();
                }
                return _instance;
            }
        }

        public static string DefaultImageURI = "rabbit.png";
    }
}

