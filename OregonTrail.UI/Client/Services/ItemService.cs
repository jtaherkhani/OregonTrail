using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using OregonTrail.Models.Shared;
using System.Threading.Tasks;
using CurrieTechnologies.Razor.SweetAlert2;

namespace OregonTrail.UI.Client.Services
{
    public class ItemService : ServerService
    {
        public ItemService(HttpClient httpClient, SweetAlertService sweetAlertService)
            : base(httpClient, sweetAlertService)
        {
            var builder = new UriBuilder(httpClient.BaseAddress);
            builder.Path += "api/Item/";
            ControllerUri = builder.Uri;
        }

        public async Task<List<Item>> GetItems()
        {
            var path = "GetItems";
            var controllerResponseDTO = await Get<List<Item>>(path);

            return controllerResponseDTO.Content;
        }

        public async Task<Item> SaveItem(Item item)
        {
            var path = "SaveItem";
            var controllerResponseDTO = await Post(path, item);

            return controllerResponseDTO.Content; // no need to deserialize as the response is already a string containing the item name
        }

        public async Task DeleteItem(Item item)
        {
            var path = "DeleteItem";
            await Post(path, item); // no need to respond on deletion
        }
    }
}
