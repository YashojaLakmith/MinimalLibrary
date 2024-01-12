using Domain.Dto;
using Domain.Exceptions;

namespace Domain.Services
{
    public interface IUserAccountService
    {
        Task<IEnumerable<UserPublicView>> GetAllUsersAsync(AdvancedUserSearch advancedSearch, CancellationToken cancellationToken = default);

        ///<exception cref="RecordNotFoundException"/>
        ///<exception cref="OperationCanceledException"/>
        Task<CurrentUser> GetCurrentUserAsync(string userId, CancellationToken cancellationToken = default);

        ///<exception cref="RecordNotFoundException"/>
        ///<exception cref="OperationCanceledException"/>
        Task<UserPublicView> GetSpecificUserAsync(string userId, CancellationToken cancellationToken = default);

        ///<exception cref="RecordNotFoundException"/>
        ///<exception cref="OperationCanceledException"/>
        Task<bool> VerifyPasswordAsync(LoginInformation loginInfo, CancellationToken cancellationToken = default);

        ///<exception cref="ValidationFailedException"/>
        ///<exception cref="AlreadyExistsException"/>
        ///<exception cref="OperationCanceledException"/>
        Task CreateUserAsync(UserCreation userCreation, CancellationToken cancellationToken = default);

        ///<exception cref="RecordNotFoundException"/>
        ///<exception cref="ValidationFailedException"/>
        ///<exception cref="OperationCanceledException"/>
        Task ChangePasswordAsync(PasswordChange changePassword, CancellationToken cancellationToken = default);

        ///<exception cref="RecordNotFoundException"/>
        ///<exception cref="ValidationFailedException"/>
        ///<exception cref="OperationCanceledException"/>
        Task ChangePasswordAsync(string userId, string newPassword, CancellationToken cancellationToken = default);

        ///<exception cref="RecordNotFoundException"/>
        ///<exception cref="ValidationFailedException"/>
        ///<exception cref="OperationCanceledException"/>
        Task ModifyUserAsync(UserModification userModification, CancellationToken cancellationToken = default);

        ///<exception cref="RecordNotFoundException"/>
        ///<exception cref="OperationCanceledException"/>
        Task DeleteUserAsync(string password,  CancellationToken cancellationToken = default);
    }
}
