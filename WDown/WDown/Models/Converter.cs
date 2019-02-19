using System;
using System.Globalization;
using Xamarin.Forms;

namespace WDown.Models
{
    public class IntEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum)
            {
                return (int)value;
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                var myReturn = Enum.ToObject(targetType, value);
                return myReturn.ToString();
            }

            return 0;
        }
    }

    // Converts from a String to the enum value.  Head = 5, would return 5 for the string "Head", and for "Head" will return 5
    public class StringEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum)
            {
                return (int)value;
            }

            if (value is string)
            {
                var aa = targetType.GetType();
                var myReturn = Enum.Parse((targetType), value.ToString());
                return myReturn;
            }

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                var myReturn = Enum.ToObject(targetType, value);
                return myReturn.ToString();
            }

            if (value is string)
            {
                var aa = targetType.GetType();
                var myReturn = Enum.Parse((targetType), value.ToString());
                return myReturn;
            }
            return 0;
        }
    }
}
