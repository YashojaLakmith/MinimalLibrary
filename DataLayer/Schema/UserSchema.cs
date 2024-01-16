using Domain;
using Domain.BaseEntities;

using MongoDB.Bson.Serialization.Attributes;

namespace DataLayer.Schema
{
    [BsonIgnoreExtraElements]
    public class UserSchema
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public Address UserAddress { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }

        public User AsEntity()
        {
            return User.CreateUser(UserId)
                            .SetUserName(UserName)
                            .SetAddress(UserAddress)
                            .SetEmail(EmailAddress)
                            .SetActivationStatus(IsActive)
                            .SetPasswordAndSalt(PasswordHash, Salt);
        }

        public static UserSchema ToUserSchema(User user)
        {
            return new UserSchema()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserAddress = user.UserAddress,
                EmailAddress = user.EmailAddress,
                IsActive = user.IsActive,
                PasswordHash = user.PasswordHash,
                Salt = user.Salt,
            };
        }
    }
}
