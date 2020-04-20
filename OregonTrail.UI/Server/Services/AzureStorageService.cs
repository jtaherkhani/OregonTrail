using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace OregonTrail.UI.Server.Services
{
    public class AzureStorageService : IFileStorageService
    {
        private readonly string ConnectionString;
        private readonly CloudBlobClient Client;

        public AzureStorageService(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("AzureStorage");
            Client = CloudStorageAccount.Parse(ConnectionString).CreateCloudBlobClient();
        }

        public async Task DeleteFile(string fileRoute, string containerName)
        {
            var container = Client.GetContainerReference(containerName);

            var blobName = Path.GetFileName(fileRoute); // find the name of the blob, then take a reference to delete it if exists
            var blob = container.GetBlobReference(blobName);
            await blob.DeleteIfExistsAsync();

        }

        public async Task<string> EditFile(byte[] content, string extension, string containerName, string fileRoute)
        {
            if (!string.IsNullOrEmpty(fileRoute))
            {
                await DeleteFile(fileRoute, containerName);
            }

            return await SaveFile(content, extension, containerName);
        }

        public async Task<string> SaveFile(byte[] content, string extension, string containerName)
        {
            var container = Client.GetContainerReference(containerName);

            await container.CreateIfNotExistsAsync();
            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            var fileName = $"{ Guid.NewGuid()}.{ extension}"; // releasing a GUID each time will reduce the chance of overlapping name to a minimum.
            var blob = container.GetBlockBlobReference(fileName);

            await blob.UploadFromByteArrayAsync(content, 0, content.Length); // write everything into the blob
            blob.Properties.ContentType = "image/png"; // always provide a png

            await blob.SetPropertiesAsync();
            return blob.Uri.ToString();
        }
    }
}
