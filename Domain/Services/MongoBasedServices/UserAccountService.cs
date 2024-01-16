using System.Text;

using Domain.BaseEntities;
using Domain.Dto;
using Domain.Exceptions;
using Domain.Extensions;
using Domain.Services.MongoBasedServices.Schema;
using Domain.Validations;

using MongoDB.Bson;
using MongoDB.Driver;

namespace Domain.Services.MongoBasedServices
{
    public class UserAccountService : MongoBackedService, IUserAccountService
    {
        private const string COLLECTION_NAME = "UserAccounts";
        private readonly IMongoCollection<UserSchema> _accCollection;
        private readonly FilterDefinitionBuilder<UserSchema> _filterBuilder = Builders<UserSchema>.Filter;

        public UserAccountService(IMongoClient mongoClient)
        {
            IMongoDatabase db = mongoClient.GetDatabase(DB_NAME);
            _accCollection = db.GetCollection<UserSchema>(COLLECTION_NAME);
        }

        public async Task ChangePasswordAsync(PasswordChange changePassword, CancellationToken cancellationToken = default)
        {
            if (BasicValidation.ValidatePassword(changePassword.NewPassword))
            {
                throw new ValidationFailedException(nameof(changePassword.NewPassword));
            }

            var filter = _filterBuilder.Eq(user => user.UserId, changePassword.UserId);
            var userAsSchema = await _accCollection.Find(filter)
                                                .SingleOrDefaultAsync(cancellationToken) ?? throw new RecordNotFoundException("User");

            var userAsEntity = userAsSchema.AsUser();
            if (userAsEntity.VerifyPassword(Encoding.ASCII.GetBytes(changePassword.CurrentPassword)))
            {
                throw new AuthenticationException();
            }

            userAsEntity.SetPassword(Encoding.ASCII.GetBytes(changePassword.NewPassword));
            userAsSchema = UserSchema.ToUserSchema(userAsEntity);
            await _accCollection.ReplaceOneAsync(filter, userAsSchema, cancellationToken: cancellationToken);
        }

        public async Task ChangePasswordAsync(string userId, string newPassword, CancellationToken cancellationToken = default)
        {
            if (BasicValidation.ValidatePassword(newPassword))
            {
                throw new ValidationFailedException(nameof(newPassword));
            }

            var filter = _filterBuilder.Eq(user => user.UserId, userId);
            var userAsSchema = await _accCollection.Find(filter)
                                                .SingleOrDefaultAsync(cancellationToken) ?? throw new RecordNotFoundException("User");

            var userAsEntity = userAsSchema.AsUser();
            userAsEntity.SetPassword(Encoding.ASCII.GetBytes(newPassword));
            userAsSchema = UserSchema.ToUserSchema(userAsEntity);
            await _accCollection.ReplaceOneAsync(filter, userAsSchema, cancellationToken: cancellationToken);
        }

        public async Task CreateUserAsync(UserCreation userCreation, CancellationToken cancellationToken = default)
        {
            if (!BasicValidation.ValidatePassword(userCreation.Password))
            {
                throw new ValidationFailedException("Password");
            }

            var filter = _filterBuilder.Eq(user => user.UserId, userCreation.UserId);
            var userAsSchema = await _accCollection.Find(filter)
                                                .SingleOrDefaultAsync(cancellationToken);
            
            if(userAsSchema is not null && userAsSchema.IsActive)
            {
                throw new AlreadyExistsException("User");
            }

            if (userAsSchema is not null && !userAsSchema.IsActive)
            {
                userAsSchema.IsActive = true;
                await _accCollection.FindOneAndReplaceAsync(filter, userAsSchema, cancellationToken: cancellationToken);
                return;
            }

            var userAsEntity = User.CreateUser(Guid.NewGuid().ToString())
                                        .SetUserName(userCreation.UserName)
                                        .SetActivationStatus(true)
                                        .SetAddress(Address.CreateAddress(userCreation.AddressLine1, userCreation.AddressLine2))
                                        .SetEmail(userCreation.EmailAddress);
            userAsEntity.SetPassword(Encoding.ASCII.GetBytes(userCreation.Password));

            await _accCollection.InsertOneAsync(UserSchema.ToUserSchema(userAsEntity), cancellationToken: cancellationToken);
        }

        public async Task DeleteUserAsync(string userId, string password, CancellationToken cancellationToken = default)
        {
            if (BasicValidation.ValidatePassword(password))
            {
                throw new ValidationFailedException(nameof(password));
            }

            var filter = _filterBuilder.Eq(user => user.UserId, userId);
            var userAsSchema = await _accCollection.Find(filter)
                                                .SingleOrDefaultAsync(cancellationToken) ?? throw new RecordNotFoundException("User");

            var userAsEntity = userAsSchema.AsUser();
            if (!userAsEntity.VerifyPassword(Encoding.ASCII.GetBytes(password)))
            {
                throw new AuthenticationException();
            }

            userAsEntity.SetActivationStatus(false);
            await _accCollection.ReplaceOneAsync(filter, UserSchema.ToUserSchema(userAsEntity), cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<UserPublicView>> GetAllUsersAsync(AdvancedUserSearch advancedSearch, CancellationToken cancellationToken = default)
        {
            BasicValidation.ValidatePagination(new Pagination(advancedSearch.PageNo, advancedSearch.ResultCount));
            int take = advancedSearch.ResultCount;
            int skip = (advancedSearch.PageNo - 1) * take;
            FilterDefinition<UserSchema> userNameFilter;

            if (!string.IsNullOrEmpty(advancedSearch.UserName))
            {
                userNameFilter = _filterBuilder.Regex(user => user.UserName, $"\\{advancedSearch.UserName}\\b");
            }
            else
            {
                userNameFilter = _filterBuilder.Regex(user => user.UserName, ".");
            }

            return (await _accCollection.Find(new BsonDocument())
                                    .Skip(skip)
                                    .Limit(take)
                                    .ToListAsync(cancellationToken))
                                    .Select(x => x.AsUser().AsUserPublicView());
        }

        public async Task<CurrentUser> GetCurrentUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            var filter = _filterBuilder.Eq(user => user.UserId, userId);

            var userAsSchema = await _accCollection.Find(filter)
                                                    .FirstOrDefaultAsync(cancellationToken) ?? throw new RecordNotFoundException("User");
            var userAsEntity = userAsSchema.AsUser();

            return null;
        }

        public Task<UserPublicView> GetSpecificUserAsync(string userId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task ModifyUserAsync(UserModification userModification, CancellationToken cancellationToken = default)
        {
            if (BasicValidation.ValidatePassword(userModification.Password))
            {
                throw new ValidationFailedException(nameof(userModification.Password));
            }

            var filter = _filterBuilder.Eq(user => user.UserId, userModification.UserId);
            var userAsSchema = await _accCollection.Find(filter)
                                                .SingleOrDefaultAsync(cancellationToken) ?? throw new RecordNotFoundException("User");

            userAsSchema.UserName = userModification.UserName;
            userAsSchema.EmailAddress = userModification.EmailAddress;
            userAsSchema.UserAddress = Address.CreateAddress(userModification.AddressLine1, userModification.AddressLine2);

            await _accCollection.FindOneAndReplaceAsync(filter, userAsSchema, cancellationToken: cancellationToken);
        }

        public async Task<bool> VerifyPasswordAsync(LoginInformation loginInfo, CancellationToken cancellationToken = default)
        {
            if (BasicValidation.ValidatePassword(loginInfo.Password))
            {
                throw new ValidationFailedException(nameof(loginInfo.Password));
            }

            var filter = _filterBuilder.Eq(user => user.UserId, loginInfo.UserId);
            var userAsSchema = await _accCollection.Find(filter)
                                                .SingleOrDefaultAsync(cancellationToken) ?? throw new RecordNotFoundException("User");
            
            return userAsSchema.AsUser().VerifyPassword(Encoding.ASCII.GetBytes(loginInfo.Password));
        }
    }
}
