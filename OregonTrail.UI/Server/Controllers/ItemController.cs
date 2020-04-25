using OregonTrail.Data.Context;
using OregonTrail.Data.Services;
using OregonTrail.Models.Shared;
using OregonTrail.UI.Server.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OregonTrail.UI.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ItemController : Controller
    {
        private readonly ItemDataService DataService;
        private readonly IFileStorageService StorageService;

        public ItemController(OregonTrailDBContext dbContext, IFileStorageService fileStorageService)
        {
            DataService = new ItemDataService(dbContext);
            StorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<List<Item>> GetItems()
        {
            return await DataService.GetItems();
        }

        [HttpPost]
        public async Task<ActionResult<string>> SaveItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            // If the Image field is populated the user must have provided a value, 
            // otherwise the Image field will be null with a URL provided instead.
            if (!string.IsNullOrEmpty(item.Image)) 
            {
                var itemImage = Convert.FromBase64String(item.Image);
                item.Image = await StorageService.SaveFile(itemImage, "png", "items"); // todo: allow for other file extensions.
            }

            return await DataService.SaveItem(item);
        }

        [HttpPost]
        public async Task<ActionResult<string>> DeleteItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!string.IsNullOrEmpty(item.Image))
            {
                await StorageService.DeleteFile(item.Image, "items");
            }

            return await DataService.DeleteItem(item);
        }
    }
}
