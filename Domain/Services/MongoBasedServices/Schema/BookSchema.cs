using Domain.BaseEntities;

using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Services.MongoBasedServices.Schema
{
    [BsonIgnoreExtraElements]
    internal class BookSchema
    {
        public string BookId { get; set; }
        public string BookName { get; set; }
        public string ISBN { get; set; }
        public string OwnerId { get; set; }
        public string? CurrentHolderId { get; set; }
        public string BookImgUrl { get; set; }
        public string[] Authors { get; set; }
        public BookAvailability BookAvailability { get; set; }
    }
}
