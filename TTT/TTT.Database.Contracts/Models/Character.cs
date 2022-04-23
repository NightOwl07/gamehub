using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using AltV.Net.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TTT.Database.Contracts.Attributes;
using TTT.Database.Contracts.Interfaces.Base;

namespace TTT.Database.Contracts.Models
{
    [BsonCollection("characters")]
    public class Character : IBaseDatabaseItem
    {
        public ObjectId Id { get; set; }
        
        public int AccountId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double PlayTime { get; set; }

        public float LastPosX { get; set; }

        public float LastPosY { get; set; }

        public float LastPosZ { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastUse { get; set; }
        
        public CharacterAppearance Appearance { get; set; }

        [BsonIgnore] public Inventory Inventory { get; set; }

        [BsonIgnore] public Dictionary<BodyPart, int> Injuries { get; set; }

        [BsonIgnore]
        public Vector3 LastPosition
        {
            get => new(this.LastPosX, this.LastPosY, this.LastPosZ);
            set
            {
                this.LastPosX = value.X;
                this.LastPosY = value.X;
                this.LastPosZ = value.X;
            }
        }

        [BsonIgnore] public string FullName => $"{this.FirstName} {this.LastName}";
    }
}