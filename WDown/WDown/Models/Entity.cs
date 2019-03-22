using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace WDown.Models
{
    // Child class of Entity 
    
    public class Entity<T> : BaseEntity<T>
    {
        // All entity has name
        public string Name { get; set; }

        // Description for current entity
        public string Description { get; set; }

        // Image for entity
        public string ImageURI { get; set; }

        //get image uri 
        public string GetImageURI()
        {
            return ImageURI;
        }
    }
}