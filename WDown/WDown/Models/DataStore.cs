using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace WDown.Models
{
    // What data store will be used.  
    public enum DataStoreEnum
    {
        Unknown = 0,
        SQL = 1,
        Mock = 2
    }
}