using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OregonTrail.Data.Context;
using OregonTrail.Models;
using Microsoft.EntityFrameworkCore;

namespace OregonTrail.Data.Services
{
    public class ItemDataService
    {
        private readonly OregonTrailDBContext context;

        public ItemDataService(OregonTrailDBContext dbContext)
        {
            context = dbContext;
        }

        /// <summary>
        /// Retrieves all items in the database as a list.
        /// </summary>
        /// <returns>A list of items.</returns>
        public async Task<List<Item>> GetItems()
        {
            return await context.Items.ToListAsync();
        }

        /// <summary>
        /// Retrieves the item from the database.
        /// </summary>
        /// <param name="item">An item to find.</param>
        /// <returns></returns>
        public async Task<Item> GetItem(Item item)
        {
            return await context.Items.Where(x => x.Id == item.Id).FirstOrDefaultAsync<Item>();
        }

        /// <summary>
        /// Creates or edits an item in the database.
        /// </summary>
        /// <param name="item">Item to be added into the database.</param>
        /// <returns>The name of the created item.</returns>
        public async Task<string> SaveItem(Item item)
        {
            // Todo: Investigate if we can have more robust error handling here.
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var existingItem = await GetItem(item);

            if (existingItem != null)
            {
                existingItem.Name = item.Name;
                existingItem.Points = item.Points;
                
                if (item.Image != null) // if the image is NULL then we know it doesn't need to be copied.
                {
                    existingItem.Image = item.Image;
                }
            }
            else
            {
                context.Items.Add(item);
            }

            await context.SaveChangesAsync();
            return item.Name;
        }

        /// <summary>
        /// Deletes an item from the database.
        /// </summary>
        /// <param name="itemToDelete">Item to be deleted from the database.</param>
        /// <returns>A task representing the deletion of the item.</returns>
        public async Task<string> DeleteItem(Item itemToDelete)
        {
            if (itemToDelete == null)
            {
                throw new ArgumentNullException(nameof(itemToDelete));
            }

            context.Remove(itemToDelete);
            await context.SaveChangesAsync();
            return itemToDelete.Name;
        }
    }
}
