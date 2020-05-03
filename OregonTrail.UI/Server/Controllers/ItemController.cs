using OregonTrail.Data.Context;
using OregonTrail.Data.Services;
using OregonTrail.Models.Shared;
using OregonTrail.UI.Server.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OregonTrail.UI.Shared.DTOs;

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
        public async Task<ControllerResponseDTO<List<Item>>> GetItems()
        {
            return new ControllerResponseDTO<List<Item>>()
            {
                Content = await DataService.GetItems()
            };
        }

        [HttpPost]
        public async Task<ControllerResponseDTO<ActionResult<Item>>> SaveItem(Item item)
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

            return new ControllerResponseDTO<ActionResult<Item>>()
            {
                Content = await DataService.SaveItem(item)
            };
        }

        [HttpPost]
        public async Task<ControllerResponseDTO<ActionResult<Item>>> DeleteItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!string.IsNullOrEmpty(item.Image))
            {
                await StorageService.DeleteFile(item.Image, "items");
            }

            return new ControllerResponseDTO<ActionResult<Item>>()
            { 
                Content = await DataService.DeleteItem(item)
            };
        }
    }
}
