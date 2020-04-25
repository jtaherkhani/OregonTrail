using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using OregonTrail.Models.Shared;
using OregonTrail.UI.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OregonTrail.UI.Client.Services
{
    public class UserService : ServerService
    {
        public UserService(HttpClient httpClient)
           : base(httpClient)
        {
            var builder = new UriBuilder(httpClient.BaseAddress);
            builder.Path += "api/User/";
            ControllerUri = builder.Uri;
        }

        public async Task<PaginationResponseDTO<List<UserRoleView>>> GetPaginatedUserRoles(PaginationRequstDTO requestDTO)
        {
            var path = "GetPaginatedUserRoles";
            var json = await GetPaginated(path, requestDTO);

            return JsonConvert.DeserializeObject<PaginationResponseDTO<List<UserRoleView>>>(json);
        }
    }
}
