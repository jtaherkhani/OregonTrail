using Blazor.FileReader;
using Radzen.Blazor;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace OregonTrail.UI.Client.Shared
{
    public class InputImageCode : ComponentBase
    {
        [Inject]
        public IFileReaderService FileReaderService { get; set; }

        [Parameter]
        public string Label { get; set; } = "Image";

        [Parameter]
        public string ImageURL { get; set; }

        [Parameter]
        public EventCallback<string> OnSelectedImage { get; set; }

        public ElementReference InputElement;
        public string ImageBase64;

        public async Task ImageFileSelected()
        {
            foreach (var file in await FileReaderService.CreateReference(InputElement).EnumerateFilesAsync())
            {
                using var memoryStream = await file.CreateMemoryStreamAsync(4 * 1024);// todo: determine the right sizing for this and move to constant.

                var imageBytes = new byte[memoryStream.Length];
                memoryStream.Read(imageBytes, 0, (int)memoryStream.Length);
                ImageBase64 = Convert.ToBase64String(imageBytes);
                await OnSelectedImage.InvokeAsync(ImageBase64);

                // Keep the URL in sync
                ImageURL = null;
                StateHasChanged();
            }
        }
    }
}
