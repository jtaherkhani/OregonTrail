using Microsoft.AspNetCore.Components;
using OregonTrail.Models;
using System.Threading.Tasks;
using Radzen;

namespace OregonTrail.UI.Client.Pages.Modals
{
    public class ItemModalCode : ComponentBase
    {
        [Inject]
        public DialogService DialogService { get; set; }

        [Parameter]
        public Item Item { get; set; }

        protected string CreatedItemMessage;

        /// <summary>
        /// Stores the image url if it can be found and provides to the InputImage razor component.
        /// </summary>
        protected string ImageURL;

        protected override Task OnInitializedAsync()
        {
            Item ??= new Item();

            if (!string.IsNullOrEmpty(Item.Image))
            {
                ImageURL = Item.Image;
                Item.Image = null; // enables the db to recognize the image does not need to be updated
            }


            return base.OnInitializedAsync();
        }

        protected void ItemImageSelected(string imageBase64)
        {
            Item.Image = imageBase64;
            ImageURL = null;
        }

        public void Save()
        {
            DialogService.Close(DialogResult<Item>.Ok(Item));
        }

        public void Cancel()
        {
            DialogService.Close(DialogResult<Item>.Cancel());
        }

    }
}
