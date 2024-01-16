namespace Domain.DataAccess
{
    public interface IBookDataAccess
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(string? nameFilter, string? authorFilter, int skip, int take, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetListedBooksOfUserAsync(string userId, int skip, int take, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetBooksBorrowedFromUserAsync(string userId, int skip, int take, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetBooksBorrowedToUserAsync(string userId, int skip, int take, CancellationToken cancellationToken = default);
        Task<Book?> GetBookByIdAsync(string bookId,  CancellationToken cancellationToken = default);
        Task CreateBookAsync(Book book, CancellationToken cancellationToken = default);
        Task ModifyBookAsync(Book book, CancellationToken cancellationToken = default);
        Task DeleteBookAsync(string bookId, CancellationToken cancellationToken = default);
    }
}
