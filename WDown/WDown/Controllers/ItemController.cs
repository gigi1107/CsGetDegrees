using System;
using System.Collections.Generic;
using WDown.Services;
using WDown.Models;
using WDown.ViewModels;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace WDown.Controllers
{

    public class ItemController
    {
        // Make this a singleton so it only exist one time because holds all the data records in memory
        private static ItemController _instance;

        public static ItemController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ItemController();
                }
                return _instance;
            }
        }

        // Return the Default Image URI for the Local Image for an Item.
        public static string DefaultImageURI = "Item.png";

        #region ServerCalls
        public async Task<List<Item>> GetItemsFromServer(int parameter = 100)
        {
            // parameter is the item group to request.  1, 2, 3, 100

            // URL to get items from the server

            var URLComponent = "GetItemList/";

            // Get result from Server
            var DataResult = await HttpClientService.Instance.GetJsonGetAsync(WebGlobals.WebSiteAPIURL + URLComponent + parameter);

            // Parse them
            var myList = ParseJson(DataResult);
            if (myList == null)
            {
                // Error, no results
                return null;
            }

            // Then update the database

            // Use a foreach on myList
            foreach (var item in myList)
            {
                // Call to the View Model (that is where the datasource is set, and have it then save
                await ItemsViewModel.Instance.InsertUpdateAsync(item);
            }

            // When foreach is done, call to the items view model to set needs refresh to true, so it can refetch the list...
            ItemsViewModel.Instance.SetNeedsRefresh(true);

            return myList;
        }

        // Asks the server for items based on paramaters
        // Number is th enumber of items to return
        // Level is the Value max for the items
        // Random is to have the value random between 1 and the Level
        // Attribute is a filter to return only items for that attribute, else unknown is used for any
        // Location is a filter to return only items for that location, else unknown is used for any
        public async Task<List<Item>> GetItemsFromGame(int number, int level, AttributeEnum attribute, ItemLocationEnum location, bool random, bool updateDataBase)
        {
            // Get items from server

            var URLComponent = "GetItemListPost";

            var dict = new Dictionary<string, string>
            {
                { "Number", number.ToString()},
                { "Level", level.ToString()},
                { "Attribute", ((int)attribute).ToString()},
                { "Location", ((int)location).ToString()},
                { "Random", random.ToString()}

            };

            // Convert parameters to a key value pairs to a json object
            JObject finalContentJson = (JObject)JToken.FromObject(dict);

            // Make a call to the helper.  URL and Parameters
            var DataResult = await HttpClientService.Instance.GetJsonPostAsync(WebGlobals.WebSiteAPIURL + URLComponent, finalContentJson);

            // Parse them
            var myList = ParseJson(DataResult);
            if (myList == null)
            {
                // Error, no results, return empty list.
                return new List<Item>();
            }

            // Then update the database

            // Use a foreach on myList
            if (updateDataBase)
            {
                foreach (var item in myList)
                {
                    // Call to the View Model (that is where the datasource is set, and have it then save
                    await ItemsViewModel.Instance.InsertUpdateAsync(item);
                }

                // When foreach is done, call to the items view model to set needs refresh to true, so it can refetch the list...
                ItemsViewModel.Instance.SetNeedsRefresh(true);
            }

            return myList;
        }

        // Parsing Json string into a list of item
        private List<Item> ParseJson(string myJsonData)
        {
            var myData = new List<Item>();

            try
            {
                JObject json;
                json = JObject.Parse(myJsonData);

                // Data is a List of Items, so need to pull them out one by one...

                var myTempList = json["ItemList"].ToObject<List<JObject>>();

                foreach (var myItem in myTempList)
                {
                    var myTempObject = ConvertFromJson(myItem);
                    if (myTempObject != null)
                    {
                        myData.Add(myTempObject);
                    }
                }

                return myData;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                return null;
            }

        }
        // Convert Json object to an Item class 
        // to avoid Json object's missing or additional fields
        private Item ConvertFromJson(JObject json)
        {
            var myData = new Item();

            try
            {
                myData.Name = JsonHelper.GetJsonString(json, "Name");
                myData.Guid = JsonHelper.GetJsonString(json, "Guid");
                myData.Id = myData.Guid;    // Set to be the same as Guid, does not come down from server, but needed for DB

                myData.Description = JsonHelper.GetJsonString(json, "Description");
                myData.ImageURI = JsonHelper.GetJsonString(json, "ImageURI");

                myData.Value = JsonHelper.GetJsonInteger(json, "Value");
                myData.Range = JsonHelper.GetJsonInteger(json, "Range");
                myData.Damage = JsonHelper.GetJsonInteger(json, "Damage");

                myData.Location = (ItemLocationEnum)JsonHelper.GetJsonInteger(json, "Location");
                myData.Attribute = (AttributeEnum)JsonHelper.GetJsonInteger(json, "Attribute");

            }

            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                return null;
            }

            return myData;
        }
        #endregion ServerCalls
    }
}
