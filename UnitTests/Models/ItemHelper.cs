using Xamarin.Forms.Mocks;
using WDown.Models;
using WDown.ViewModels;

namespace UnitTests.Models
{
    public static class ItemHelper
    {
        public static Item AddItemForAttribute(AttributeEnum attributeEnum, ItemLocationEnum itemLocationEnum, int value)
        {

            MockForms.Init();

            var myItem = new Item
            {
                Attribute = attributeEnum,
                Location = itemLocationEnum,
                Value = value
            };

            ItemsViewModel.Instance.AddAsync(myItem).GetAwaiter().GetResult();  // Register Item to DataSet

            return myItem;
        }
    }
}