using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OregonTrail.Data.Context;
using OregonTrail.Data.Services;
using OregonTrail.Models.Shared;
using OregonTrail.UI.Shared.DTOs;
using OregonTrail.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace OregonTrail.UI.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class UserController : Controller
    {
        private readonly UserDataService DataService;

        public UserController(OregonTrailDBContext dbContext)
        {
            DataService = new UserDataService(dbContext);
        }

        [HttpGet]
        public PaginationResponseDTO<List<UserRoleView>> GetPaginatedUserRoles([FromQuery] PaginationRequstDTO paginationRequest)
        {
            var allQueryableUsers = DataService.GetQueryableUserRoles();
            var paginationResponseDTO = new PaginationResponseDTO<List<UserRoleView>>()
            {
                Content = allQueryableUsers.Paginate(paginationRequest).ToList(),
                TotalRecordCount = allQueryableUsers.Count()
            };

            return new PaginationResponseDTO<List<UserRoleView>>()
            {
                Content = allQueryableUsers.Paginate(paginationRequest).ToList(),
                TotalRecordCount = allQueryableUsers.Count()
            };
        }
    }
}
