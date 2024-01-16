using DataLayer.Schema;

using Domain;
using Domain.DataAccess;

using MongoDB.Driver;

namespace DataLayer
{
    public class UserDataAccess : MongoDbBackedDataAccess, IUserDataAccess
    {
        private const string COLLECTION_NAME = "UserAccountCollection";
        private readonly IMongoCollection<UserSchema> _userCollection;
        private readonly FilterDefinitionBuilder<UserSchema> _filterBuilder = Builders<UserSchema>.Filter;

        public UserDataAccess(IMongoClient mongoClient)
        {
            IMongoDatabase db = mongoClient.GetDatabase(DB_NAME);
            _userCollection = db.GetCollection<UserSchema>(COLLECTION_NAME);
        }

        public async Task CreateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            await _userCollection.InsertOneAsync(UserSchema.ToUserSchema(user), new InsertOneOptions(), cancellationToken);
        }

        public async Task DeleteUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            var res = await _userCollection.DeleteOneAsync(u => u.UserId == userId, cancellationToken);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(string? nameFilter, int skip, int take, CancellationToken cancellationToken = default)
        {
            var filter = _filterBuilder.Empty;
            if(nameFilter is not null)
            {
                filter = _filterBuilder.Where(x => x.UserName.Contains(nameFilter, StringComparison.OrdinalIgnoreCase));
            }

            return (await _userCollection.Find(filter)
                                            .Skip(skip)
                                            .Limit(take)
                                            .ToListAsync())
                                            .Select(x => x.AsEntity());
        }

        public async Task<User?> GetUserByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            return (await _userCollection.Find(u => u.UserId == userId)
                                            .SingleOrDefaultAsync(cancellationToken))?
                                            .AsEntity();
        }

        public async Task ModifyUserAsync(User user, CancellationToken cancellationToken = default)
        {
            await _userCollection.ReplaceOneAsync(u => u.UserId == user.UserId, UserSchema.ToUserSchema(user), cancellationToken: cancellationToken);
        }
    }
}
