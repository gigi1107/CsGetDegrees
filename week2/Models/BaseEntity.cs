using System;

using Xamarin.Forms;

namespace week2.Models
{
    public class BaseEntity<T>
    {
        public string Id { get; set; }

        public string Guid { get; set; }

        public BaseEntity()
        {
            Guid = System.Guid.NewGuid().ToString();
            Id = Guid;
        }
    }

  
}

