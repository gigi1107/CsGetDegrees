using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace WDown.Models
{
    // Base type for Item, Monster, Character, and Score
    public class BaseEntity<T>
    {
        [PrimaryKey]

        // Each entity has an ID and Guid for the database
        public string Id { get; set; }

        public string Guid { get; set; }

        public BaseEntity()
        {
            Guid = System.Guid.NewGuid().ToString();
            Id = Guid;
        }
    }


}

