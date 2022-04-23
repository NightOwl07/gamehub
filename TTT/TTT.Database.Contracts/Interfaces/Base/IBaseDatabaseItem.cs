using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TTT.Database.Contracts.Interfaces.Base
{
    public interface IBaseDatabaseItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId Id { get; }
    }
}