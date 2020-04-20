using System.Threading.Tasks;

namespace OregonTrail.UI.Server.Services
{
    /// <summary>
    /// Interface to define actions of a file storage service.
    /// </summary>
    public interface IFileStorageService
    {
        /// <summary>
        /// Contains definitions for editing a file.
        /// </summary>
        /// <param name="content">Content of the file.</param>
        /// <param name="extension">File extension type.</param>
        /// <param name="containerName">The name of the container.</param>
        /// <param name="fileRoute">The route to the file.</param>
        /// <returns>A string representing the edited file url.</returns>
        public Task<string> EditFile(byte[] content, string extension, string containerName, string fileRoute);

        /// <summary>
        /// Contains definitions for deleting a file.
        /// </summary>
        /// <param name="fileRoute">The route to the file.</param>
        /// <param name="containerName">THe name of the container.</param>
        /// <returns>A deleted file response.</returns>
        public Task DeleteFile(string fileRoute, string containerName);

        /// <summary>
        /// Contains definitions for saving a file.
        /// </summary>
        /// <param name="content">Content of the file.</param>
        /// <param name="extension">File extension type.</param>
        /// <param name="containerName">Container name.</param>
        /// <returns>The url of the file that has been saved.</returns>
        public Task<string> SaveFile(byte[] content, string extension, string containerName);
    }
}
