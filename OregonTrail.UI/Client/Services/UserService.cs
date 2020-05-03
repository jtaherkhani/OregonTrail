using CurrieTechnologies.Razor.SweetAlert2;
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
        public UserService(HttpClient httpClient, SweetAlertService sweetAlertService)
           : base(httpClient, sweetAlertService)
        {
            var builder = new UriBuilder(httpClient.BaseAddress);
            builder.Path += "api/User/";
            ControllerUri = builder.Uri;
        }

        public async Task<ControllerResponseDTO<List<UserRoleView>>> GetPaginatedUserRoles(PaginationRequstDTO requestDTO)
        {
            var path = "GetPaginatedUserRoles";
            return await GetPaginated<List<UserRoleView>>(path, requestDTO);
        }

        public async Task<ControllerResponseDTO<UserRoleView>> DeleteUserFromUserRoleView(UserRoleView userRoleView)
        {
            var path = "DeleteUserByUserRoleView";
            return await Post(path, userRoleView);
        }
    }
}
