using System;
using MongoDB.Bson;
using TTT.Contracts.Base.Enums;
using TTT.Database.Contracts.Attributes;
using TTT.Database.Contracts.Interfaces.Base;

namespace TTT.Database.Contracts.Models
{
    [BsonCollection("accounts")]
    public class Account : IBaseDatabaseItem
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public ulong SocialClubId { get; set; }

        public ulong HardwareId { get; set; }

        //public ulong HardwareIdEx { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastLogin { get; set; }

        public bool CanResetPassword { get; set; }

        public PermissionLevel PermissionLevel { get; set; }

        public ObjectId Id { get; set; }
    }
}