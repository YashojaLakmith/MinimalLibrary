using Domain.BaseEntities;
using Domain.DataAccess;
using Domain.Dto;
using Domain.Exceptions;
using Domain.Validations;

namespace Domain.Services.DefaultImplementations
{
    public class DefaultUserAccountService : IUserAccountService
    {
        private readonly IUserDataAccess _userData;

        public DefaultUserAccountService(IUserDataAccess userData)
        {
            _userData = userData;
        }

        public async Task ChangePasswordAsync(PasswordChange changePassword, CancellationToken cancellationToken = default)
        {
            // Needs password validation

            var user = await _userData.GetUserByIdAsync(changePassword.UserId, cancellationToken) ?? throw new RecordNotFoundException("User");
            if (!user.IsActive)
            {
                throw new RecordNotFoundException("User");
            }

            var oldPasswordBytes = Utility.GetPasswordBytes(changePassword.CurrentPassword);
            if(!user.VerifyPassword(oldPasswordBytes))
            {
                throw new AuthenticationException();
            }

            var newPasswordBytes = Utility.GetPasswordBytes(changePassword.NewPassword);
            user.SetPassword(newPasswordBytes);
            await _userData.ModifyUserAsync(user, cancellationToken);
        }

        public async Task ChangePasswordAsync(string userId, string newPassword, CancellationToken cancellationToken = default)
        {
            // Needs password vaidation

            var user = await _userData.GetUserByIdAsync(userId, cancellationToken) ?? throw new RecordNotFoundException("User");
            var passwordBytes = Utility.GetPasswordBytes(newPassword);
            user.SetPassword(passwordBytes);
            user.SetActivationStatus(true);
            
            await _userData.ModifyUserAsync(user, cancellationToken);
        }

        public async Task CreateUserAsync(UserCreation userCreation, CancellationToken cancellationToken = default)
        {
            // Needs validation
            // If the account is marked as deleted, refuse creation.

            var existingUser = await _userData.GetUserByIdAsync(userCreation.UserId, cancellationToken);
            if(existingUser is not null)
            {
                throw new AlreadyExistsException("User");
            }

            var passwordBytes = Utility.GetPasswordBytes(userCreation.Password);
            var newUser = User.CreateUser(userCreation.UserId)
                                .SetUserName(userCreation.UserName)
                                .SetAddress(Address.CreateAddress(userCreation.AddressLine1, userCreation.AddressLine2))
                                .SetEmail(userCreation.EmailAddress);
            newUser.SetPassword(passwordBytes);
            
            await _userData.CreateUserAsync(newUser, cancellationToken);
        }

        public async Task DeleteUserAsync(string userId, string password, CancellationToken cancellationToken = default)
        {
            // Does not delete. Marks as deleted
            // Needs password validation

            var user = await _userData.GetUserByIdAsync(userId, cancellationToken) ?? throw new RecordNotFoundException("User");

            if (!user.IsActive)
            {
                throw new RecordNotFoundException("User");
            }

            user.SetActivationStatus(false);
            await _userData.ModifyUserAsync(user, cancellationToken);
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

        public async Task ModifyUserAsync(UserModification userModification, CancellationToken cancellationToken = default)
        {
            // Needs validations

            var user = await _userData.GetUserByIdAsync(userModification.UserId, cancellationToken) ?? throw new RecordNotFoundException("User");

            if (!user.IsActive)
            {
                throw new RecordNotFoundException("User");
            }

            var passwordBytes = Utility.GetPasswordBytes(userModification.Password);
            if(!user.VerifyPassword(passwordBytes))
            {
                throw new ValidationFailedException("Password");
            }

            user = user.SetUserName(userModification.UserName)
                        .SetAddress(Address.CreateAddress(userModification.AddressLine1, userModification.AddressLine2))
                        .SetEmail(userModification.EmailAddress);

            await _userData.ModifyUserAsync(user, cancellationToken);
        }

        public async Task<bool> VerifyPasswordAsync(LoginInformation loginInfo, CancellationToken cancellationToken = default)
        {
            if (!BasicValidation.ValidatePassword(loginInfo.Password))
            {
                throw new ValidationFailedException("Password");
            }

            var user = await _userData.GetUserByIdAsync(loginInfo.UserId, cancellationToken) ?? throw new RecordNotFoundException("User");
            var passwordBytes = Utility.GetPasswordBytes(loginInfo.Password);

            return user.VerifyPassword(passwordBytes);
        }
    }
}
