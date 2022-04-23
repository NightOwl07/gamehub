using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TTT.Database.Contracts.Attributes;
using TTT.Database.Contracts.Interfaces.Base;

namespace TTT.Database.Contracts.Models
{
    [BsonCollection("inventories")]
    public class Inventory : IBaseDatabaseItem
    {
        public ObjectId Id { get; set; }
        
        public int CharacterId { get; set; }

        [BsonIgnore] public List<object> Items { get; set; }
    }
}