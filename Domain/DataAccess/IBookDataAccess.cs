namespace Domain.DataAccess
{
    public interface IBookDataAccess
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(string nameFilter, int skip, int take, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetListedBooksOfUser(string userId, int skip, int take, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetBooksBorrowedByUser(string userId, int skip, int take, CancellationToken cancellationToken = default);
        Task<IEnumerable<Book>> GetBooksBorrowedToUser(string userId, int skip, int take, CancellationToken cancellationToken = default);
        Task<Book?> GetBookByIdAsync(string bookId,  CancellationToken cancellationToken = default);
        Task CreateBookAsync(Book book, CancellationToken cancellationToken = default);
        Task ModifyBookAsync(Book book, CancellationToken cancellationToken = default);
        Task DeleteBookAsync(string bookId, CancellationToken cancellationToken = default);
    }
}
