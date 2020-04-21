using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Threading.Tasks;

namespace OregonTrail.UI.Client.Services
{
    /// <summary>
    /// Defines the service framework class utilized to speak to the server.
    /// </summary>
    public class ServerService
    {
        public HttpClient Client { get; } // publicly accessible read-only client.
        protected Uri ControllerUri; // Uri for the specific controller

        public ServerService(HttpClient httpClient)
        {
            Client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Parses the response message and presents errors if neccessary.
        /// </summary>
        /// <param name="responseMessage">The response from the server.</param>
        /// <returns>The response message.</returns>
        private async Task<string> ParseResponse(HttpResponseMessage responseMessage)
        {
            if (responseMessage == null)
            {
                throw new ArgumentNullException(nameof(responseMessage));
            }

            var json = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                //todo: implement sweet alerts here
                throw new ApplicationException(json);
            }

            //todo: consider throwing custom errors here once solution becomes more robust
            return json;
        }

        private UriBuilder CreateUriBuilder(string actionPath)
        {
            var builder = new UriBuilder(ControllerUri);
            builder.Path += actionPath;

            return builder;
        }

        /// <summary>
        /// Creates the http string content for post messages from generic content.
        /// </summary>
        /// <typeparam name="T">An instance of the object to be serialized.</typeparam>
        /// <param name="content">The content to be serialized into the request.</param>
        /// <returns>The http string content to be sent to the server.</returns>
        private StringContent CreateStringContent<T>(T content)
        {
            var contentAsJsonString = JsonConvert.SerializeObject(content);
            var stringContent = new StringContent(contentAsJsonString);

            stringContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Json);

            return stringContent; 
        }

        /// <summary>
        /// Abstraction of http get request structure to ensure the parse response is always called.
        /// </summary>
        /// <param name="actionPath">The api path excluding the initial controller path.</param>
        /// <returns>The parsed json that will then be deserialized by the caller.</returns>
        protected async Task<string> Get(string actionPath)
        {
            var builder = CreateUriBuilder(actionPath);

            var httpResponse = await Client.GetAsync(builder.Uri);
            return await ParseResponse(httpResponse);
        }

        /// <summary>
        /// Abstraction of http post request structure to ensure the parse response is always called
        /// </summary>
        /// <param name="actionPath">The api path excluding the initial controller path.</param>
        /// <param name="content">The generic content for the post request.</param>
        /// <returns>The parsed json that will then be deserialized by the caller</returns>
        protected async Task<string> Post<T>(string actionPath, T content)
        {
            var builder = CreateUriBuilder(actionPath);

            using var stringContent = CreateStringContent(content); // todo: investigate why this is a using command.

            var httpResponse = await Client.PostAsync(builder.Uri, stringContent);
            return await ParseResponse(httpResponse);
        }
    }
}
