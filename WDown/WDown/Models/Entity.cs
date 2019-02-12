using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WDown.Models
{
    public class Entity<T> : BaseEntity<T>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageURI { get; set; }
    }
}