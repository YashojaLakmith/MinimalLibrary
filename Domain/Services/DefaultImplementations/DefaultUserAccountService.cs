using Domain.BaseEntities;
using Domain.DataAccess;
using Domain.Dto;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Validations;

namespace Domain.Services.DefaultImplementations
{
    public class DefaultUserAccountService : IUserAccountService
    {
        private readonly IBookDataAccess _bookData;
        private readonly IUserDataAccess _userData;
        private readonly IInputDataValidations _dataValidations;

        public DefaultUserAccountService(IUserDataAccess userData, IBookDataAccess bookData, IInputDataValidations dataValidations)
        {
            _bookData = bookData;
            _userData = userData;
            _dataValidations = dataValidations;
        }

        public async Task ChangePasswordAsync(PasswordChange changePassword, CancellationToken cancellationToken = default)
        {
            _dataValidations.ValidateUserId(changePassword.UserId);
            _dataValidations.ValidatePassword(changePassword.CurrentPassword);
            _dataValidations.ValidatePassword(changePassword.NewPassword);

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
            _dataValidations.ValidateUserId(userId);
            _dataValidations.ValidatePassword(userId);

            var user = await _userData.GetUserByIdAsync(userId, cancellationToken) ?? throw new RecordNotFoundException("User");
            var passwordBytes = Utility.GetPasswordBytes(newPassword);
            user.SetPassword(passwordBytes);
            user.SetActivationStatus(true);
            
            await _userData.ModifyUserAsync(user, cancellationToken);
        }

        public async Task CreateUserAsync(UserCreation userCreation, CancellationToken cancellationToken = default)
        {
            _dataValidations.ValidateUserId(userCreation.UserId);
            _dataValidations.ValidatePassword(userCreation.Password);
            _dataValidations.ValidateUserName(userCreation.UserName);
            _dataValidations.ValidateEmail(userCreation.EmailAddress);
            _dataValidations.ValidateAddressLines(userCreation.AddressLine1, userCreation.AddressLine2);

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
            _dataValidations.ValidateUserId(userId);
            _dataValidations.ValidatePassword(password);

            // Does not delete. Marks as deleted
            var user = await _userData.GetUserByIdAsync(userId, cancellationToken) ?? throw new RecordNotFoundException("User");

            if (!user.IsActive)
            {
                throw new RecordNotFoundException("User");
            }

            user.SetActivationStatus(false);
            await _userData.ModifyUserAsync(user, cancellationToken);
        }

        public async Task<IEnumerable<UserPublicView>> GetAllUsersAsync(AdvancedUserSearch advancedSearch, CancellationToken cancellationToken = default)
        {
            _dataValidations.ValidatePagination(advancedSearch.PageNo, advancedSearch.ResultsCount);

            (int skip, int take) = Utility.GetSkipAndTake(advancedSearch.PageNo, advancedSearch.ResultCount);
            return (await _userData.GetAllUsersAsync(advancedSearch.UserName, skip, take, cancellationToken))
                                .Select(x => x.AsUserPublicView());
        }

        public async Task<CurrentUser> GetCurrentUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            _dataValidations.ValidateUserId(userId);

            var user = await _userData.GetUserByIdAsync(userId, cancellationToken) ?? throw new RecordNotFoundException("User");
            var books = await _bookData.GetListedBooksOfUserAsync(userId, 0, 0, cancellationToken);

            return user.SetListedBooks(books.ToList())
                            .AsCurrentUser();
        }

        public async Task<CurrentUser> GetSpecificUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            _dataValidations.ValidateUserId(userId);

            var user = await _userData.GetUserByIdAsync(userId, cancellationToken) ?? throw new RecordNotFoundException("User");
            var books = await _bookData.GetListedBooksOfUserAsync(userId, 0, 0, cancellationToken);

            return user.SetListedBooks(books.ToList())
                            .AsCurrentUser();
        }

        public async Task ModifyUserAsync(UserModification userModification, CancellationToken cancellationToken = default)
        {
            _dataValidations.ValidateUserId(userModification.UserId);
            _dataValidations.ValidatePassword(userModification.Password);
            _dataValidations.ValidateUserName(userModification.UserName);
            _dataValidations.ValidateEmail(userModification.EmailAddress);
            _dataValidations.ValidateAddressLines(userModification.AddressLine1, userModification.AddressLine2);

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
            _dataValidations.ValidateUserId(loginInfo.UserId);
            _dataValidations.ValidatePassword(loginInfo.Password);

            var user = await _userData.GetUserByIdAsync(loginInfo.UserId, cancellationToken) ?? throw new RecordNotFoundException("User");
            var passwordBytes = Utility.GetPasswordBytes(loginInfo.Password);

            return user.VerifyPassword(passwordBytes);
        }
    }
}
