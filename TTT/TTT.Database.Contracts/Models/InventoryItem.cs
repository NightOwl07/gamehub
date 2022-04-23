using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TTT.Database.Contracts.Attributes;
using TTT.Database.Contracts.Interfaces.Base;

namespace TTT.Database.Contracts.Models
{
    [BsonCollection("inventoryitems")]
    public abstract class InventoryItem : IBaseDatabaseItem
    {
        public ObjectId Id { get; set; }
        
        public int InventoryId { get; set; }

        public string Name { get; set; }

        [BsonIgnore] public string Description { get; set; }

        public string Type { get; set; }

        public int Quantity { get; set; }
    }
}