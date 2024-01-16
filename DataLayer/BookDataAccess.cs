using DataLayer.Schema;

using Domain;
using Domain.DataAccess;

using MongoDB.Driver;

namespace DataLayer
{
    public class BookDataAccess : MongoDbBackedDataAccess, IBookDataAccess
    {
        private const string COLLECTION_NAME = "BookCollection";
        private readonly IMongoCollection<BookSchema> _bookCollection;
        private readonly FilterDefinitionBuilder<BookSchema> _filterBuilder = Builders<BookSchema>.Filter;

        public BookDataAccess(IMongoClient mongoClient)
        {
            IMongoDatabase db = mongoClient.GetDatabase(DB_NAME);
            _bookCollection = db.GetCollection<BookSchema>(COLLECTION_NAME);
        }

        public async Task CreateBookAsync(Book book, CancellationToken cancellationToken = default)
        {
            await _bookCollection.InsertOneAsync(BookSchema.AsSchema(book), cancellationToken: cancellationToken);
        }

        public async Task DeleteBookAsync(string bookId, CancellationToken cancellationToken = default)
        {
            await _bookCollection.DeleteOneAsync(x => x.BookId ==  bookId, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(string? nameFilter, string? authorFilter, int skip, int take, CancellationToken cancellationToken = default)
        {
            FilterDefinition<BookSchema> filter = _filterBuilder.Empty;
            if(nameFilter is not null)
            {
                filter = _filterBuilder.Where(x => x.BookName.Contains(nameFilter));
            }

            if(authorFilter is not null)
            {
                var whereFilter = _filterBuilder.Where(x => x.Authors.Contains(authorFilter));
                filter = _filterBuilder.And(filter, whereFilter);
            }

            return (await _bookCollection.Find(filter)
                                    .ToListAsync(cancellationToken))
                                    .Select(y => y.AsEntity());
        }

        public async Task<Book?> GetBookByIdAsync(string bookId, CancellationToken cancellationToken = default)
        {
            return (await _bookCollection.Find(x => x.BookId == bookId)
                                            .FirstOrDefaultAsync(cancellationToken))?
                                            .AsEntity();
        }

        public async Task<IEnumerable<Book>> GetBooksBorrowedFromUserAsync(string userId, int skip, int take, CancellationToken cancellationToken = default)
        {
            return (await _bookCollection.Find(x => x.OwnerId == userId && !string.IsNullOrEmpty(x.CurrentHolderId))
                                            .Skip(skip)
                                            .Limit(take)
                                            .ToListAsync(cancellationToken))
                                            .Select(y => y.AsEntity());
        }

        public async Task<IEnumerable<Book>> GetBooksBorrowedToUserAsync(string userId, int skip, int take, CancellationToken cancellationToken = default)
        {
            return (await _bookCollection.Find(x => x.CurrentHolderId == userId)
                                            .Skip(skip)
                                            .Limit(take)
                                            .ToListAsync(cancellationToken))
                                            .Select(y => y.AsEntity());
        }

        public async Task<IEnumerable<Book>> GetListedBooksOfUserAsync(string userId, int skip, int take, CancellationToken cancellationToken = default)
        {
            return (await _bookCollection.Find(x => x.OwnerId == userId)
                                            .Skip(skip)
                                            .Limit(take)
                                            .ToListAsync(cancellationToken))
                                            .Select(y => y.AsEntity());
        }

        public async Task ModifyBookAsync(Book book, CancellationToken cancellationToken = default)
        {
            await _bookCollection.FindOneAndReplaceAsync(x => x.BookId == book.BookId, BookSchema.AsSchema(book), cancellationToken: cancellationToken);
        }
    }
}
