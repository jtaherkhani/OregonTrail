using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OregonTrail.Data.Context;
using OregonTrail.Data.Services;
using OregonTrail.Models.Shared;
using OregonTrail.UI.Shared.DTOs;
using OregonTrail.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OregonTrail.UI.Server.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    public class UserController : Controller
    {
        private readonly UserDataService DataService;
        private readonly UserManager<IdentityUser> UserManager;

        public UserController(OregonTrailDBContext dbContext, UserManager<IdentityUser> userManager)
        {
            DataService = new UserDataService(dbContext);
            UserManager = userManager;
        }

        [HttpGet]
        public ControllerResponseDTO<List<UserRoleView>> GetPaginatedUserRoles([FromQuery] PaginationRequstDTO paginationRequest)
        {
            var allQueryableUsers = DataService.GetQueryableUserRoles();

            return new ControllerResponseDTO<List<UserRoleView>>()
            {
                Content = allQueryableUsers.Paginate(paginationRequest).ToList(),
                TotalRecordCount = allQueryableUsers.Count()
            };
        }

        [HttpPost]
        public async Task<ControllerResponseDTO<UserRoleView>> DeleteUserByUserRoleView([FromBody] UserRoleView userRoleView)
        {
            var user = await UserManager.FindByEmailAsync(userRoleView.Email);
            var deletionResult = await UserManager.DeleteAsync(user);

            if (deletionResult.Succeeded)
            {
                return new ControllerResponseDTO<UserRoleView>()
                {
                    Content = userRoleView
                };
            }
            else
            {
                return new ControllerResponseDTO<UserRoleView>()
                {
                    Content = userRoleView,
                    ErrorMessage = deletionResult.Errors.First().Description
                };

            }
        }
    }
}
