using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using OregonTrail.Models.Shared;
using OregonTrail.UI.Client.Helpers;
using OregonTrail.UI.Client.Pages.Modals;
using OregonTrail.UI.Client.Services;
using OregonTrail.UI.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radzen;

namespace OregonTrail.UI.Client.Pages
{
    /// <summary>
    /// Code behind for the items razor page.
    /// </summary>
    public class ItemsCode : ComponentBase
    {
        [Inject]
        public DialogService DialogService { get; set; }

        [Inject]
        public SweetAlertService SweetAlertService { get; set; }

        [Inject]
        public ItemService ItemService { get; set; }

        public PaginationRequstDTO PaginationDTO = new PaginationRequstDTO(); // Todo: Should utilize the underlying constant not the pagination request/result.
        public List<Item> items;

        protected override async Task OnInitializedAsync() // todo: try after render to see if it's more performant?
        {
            DialogService.OnClose += CloseDialog; // need to map the closing of the dialog service to the close dialog method.
            items = await ItemService.GetItems();

            await base.OnInitializedAsync();
        }

        public async void NewItem()
        {
            await DialogService.OpenAsync<ItemModal>("New Item");
        }

        public async void EditItem(Item item)
        {
            // Create a dictionary to pass the item into the dialog service.
            var dialogParameters = new Dictionary<string, object>()
            { 
                { "Item", item } 
            };

            await DialogService.OpenAsync<ItemModal>("Edit Item", dialogParameters);
        }

        /// <summary>
        /// Maps the close dialog for the component.
        /// </summary>
        /// <param name="result">Dynamic class result in the Modals/DialogResult format.</param>
        public async void CloseDialog(dynamic result)
        {
            if (result != null && !result.IsCancellation) // if the modal is exited from any fashion other then cancel or save then the result will be null
            {
                await ItemService.SaveItem(result.Data);

                string successMessage;

                if (result.Data.Id != 0) // The dialog was opened for editing so a different message should be sent.
                {
                    successMessage = string.Format("Item {0}, sucessfully edited!", result.Data.Name);
                }
                else
                {
                    successMessage = string.Format("Item {0}, sucessfully created!", result.Data.Name);
                }    

                await SweetAlertService.FireAsync(AlertHelper.ToastSuccess(successMessage));

                items = await ItemService.GetItems();
                StateHasChanged();
            }
        }

        public async void DeleteItem(Item itemToDelete)
        {
            // todo: sweet alert to confirm that cancellation is desired
            var result = await SweetAlertService.FireAsync(AlertHelper.ConfirmationWarning(string.Format("Are you sure you want to delete item {0}", itemToDelete.Name)));
            
            if (!string.IsNullOrEmpty(result.Value)) // ensures that deletion only occurs after confirmation
            {
                await ItemService.DeleteItem(itemToDelete);

                items = await ItemService.GetItems();
                StateHasChanged();
            }
        }
    }
}
