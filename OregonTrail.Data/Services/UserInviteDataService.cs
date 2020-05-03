using OregonTrail.Data.Context;


namespace OregonTrail.Data.Services
{
    public class UserInviteDataService
    {
        private readonly OregonTrailDBContext context;

        public UserInviteDataService(OregonTrailDBContext dbContext)
        {
            context = dbContext;
        }
    }
}
