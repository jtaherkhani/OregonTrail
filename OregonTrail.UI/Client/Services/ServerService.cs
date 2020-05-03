using CurrieTechnologies.Razor.SweetAlert2;
using Newtonsoft.Json;
using OregonTrail.UI.Client.Helpers;
using OregonTrail.UI.Shared.DTOs;
using System;
using System.Collections.Generic;
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
        private readonly SweetAlertService SweetAlertService;

        public ServerService(HttpClient httpClient, SweetAlertService sweetAlertService)
        {
            Client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            SweetAlertService = sweetAlertService;
        }

        /// <summary>
        /// Parses the response message and presents errors if neccessary.
        /// </summary>
        /// <param name="responseMessage">The response from the server.</param>
        /// <returns>The response message.</returns>
        private async Task<ControllerResponseDTO<T>> ParseResponse<T>(HttpResponseMessage responseMessage)
        {
            if (responseMessage == null)
            {
                throw new ArgumentNullException(nameof(responseMessage));
            }

            if (!responseMessage.IsSuccessStatusCode)
            {
                // if we fail here fire an unknown sweet alert and return with an empty controller response.
                await SweetAlertService.FireAsync(AlertHelper.UnknownError());
                return new ControllerResponseDTO<T>();
            }
            
            var json = await responseMessage.Content.ReadAsStringAsync();

            var controllerResponse = JsonConvert.DeserializeObject<ControllerResponseDTO<T>>(json);

            if (controllerResponse.HasError)
            {
                await SweetAlertService.FireAsync(AlertHelper.ValidationError(controllerResponse.ErrorMessage));
            }

            //todo: consider throwing custom errors here once solution becomes more robust
            return controllerResponse;
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
        protected async Task<ControllerResponseDTO<T>> Get<T>(string actionPath)
        {
            var builder = CreateUriBuilder(actionPath);

            var httpResponse = await Client.GetAsync(builder.Uri);
            return await ParseResponse<T>(httpResponse);
        }

        protected async Task<ControllerResponseDTO<T>> GetPaginated<T>(string actionPath, PaginationRequstDTO requestDTO)
        {
            var builder = CreateUriBuilder(actionPath);

            using var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "Page", requestDTO.Page.ToString() },
                { "RecordsPerPage", requestDTO.RecordsPerPage.ToString()}
            });

            builder.Query = content.ReadAsStringAsync().Result;

            var httpResponse = await Client.GetAsync(builder.Uri);
            return await ParseResponse<T>(httpResponse);
        }

        /// <summary>
        /// Abstraction of http post request structure to ensure the parse response is always called.
        /// </summary>
        /// <typeparam name="T">Denotes the content sent to the server.</typeparam>
        /// <param name="actionPath">The api path excluding the initial controller path.</param>
        /// <param name="content">The content for the post request.</param>
        /// <returns>The parsed json in the httpResponse.</returns>
        protected async Task<ControllerResponseDTO<T>> Post<T>(string actionPath, T content)
        {
            var builder = CreateUriBuilder(actionPath);

            using var stringContent = CreateStringContent(content); // todo: investigate why this is a using command.
            var httpResponse = await Client.PostAsync(builder.Uri, stringContent);


            return await ParseResponse<T>(httpResponse);
        }
    }
}
