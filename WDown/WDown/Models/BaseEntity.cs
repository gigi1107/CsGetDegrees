using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace WDown.Models
{
    public class BaseEntity<T>
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Guid { get; set; }

        public BaseEntity()
        {
            Guid = System.Guid.NewGuid().ToString();
            Id = Guid;
        }
    }


}

