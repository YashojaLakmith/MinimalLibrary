using Domain.DataAccess;
using Domain.Dto;

namespace Domain.Services.DefaultImplementations
{
    public class DefaultUserAccountService : IUserAccountService
    {
        private readonly IUserDataAccess _userData;

        public DefaultUserAccountService(IUserDataAccess userData)
        {
            _userData = userData;
        }

        public Task ChangePasswordAsync(PasswordChange changePassword, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task ChangePasswordAsync(string userId, string newPassword, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task CreateUserAsync(UserCreation userCreation, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(string userId, string password, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserPublicView>> GetAllUsersAsync(AdvancedUserSearch advancedSearch, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<CurrentUser> GetCurrentUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<UserPublicView> GetSpecificUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task ModifyUserAsync(UserModification userModification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyPasswordAsync(LoginInformation loginInfo, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
