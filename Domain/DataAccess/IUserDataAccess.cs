namespace Domain.DataAccess
{
    public interface IUserDataAccess
    {
        Task<IEnumerable<User>> GetAllUsersAsync(string? nameFilter, int skip, int take, CancellationToken cancellationToken = default);
        Task<User?> GetUserByIdAsync(string userId,  CancellationToken cancellationToken = default);
        Task CreateUserAsync(User user, CancellationToken cancellationToken = default);
        Task ModifyUserAsync(User user, CancellationToken cancellationToken = default);
        Task DeleteUserAsync(string userId, CancellationToken cancellationToken = default);
    }
}
