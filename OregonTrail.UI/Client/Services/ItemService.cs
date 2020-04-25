using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using OregonTrail.Models.Shared;
using System.Threading.Tasks;

namespace OregonTrail.UI.Client.Services
{
    public class ItemService : ServerService
    {
        public ItemService(HttpClient httpClient)
            : base(httpClient)
        {
            var builder = new UriBuilder(httpClient.BaseAddress);
            builder.Path += "api/Item/";
            ControllerUri = builder.Uri;
        }

        public async Task<List<Item>> GetItems()
        {
            var path = "GetItems";
            var json = await Get(path);

            return JsonConvert.DeserializeObject<List<Item>>(json);
        }

        public async Task<string> SaveItem(Item item)
        {
            var path = "SaveItem";
            var result = await Post(path, item);

            return result; // no need to deserialize as the response is already a string containing the item name
        }

        public async Task DeleteItem(Item item)
        {
            var path = "DeleteItem";
            
            await Post(path, item); // no need to respond on deletion
        }
    }
}
