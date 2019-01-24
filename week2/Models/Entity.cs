using System;

using Xamarin.Forms;

namespace week2.Models
{
    public class Entity<T> : BaseEntity<T>
    {
        public string Name { get; set;  }

        public string Description { get; set; }

        public string ImageURI { get; set; }
    }
}

