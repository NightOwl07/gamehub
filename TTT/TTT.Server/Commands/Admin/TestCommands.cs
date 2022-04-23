using System;
using CustomCommandsSystem.Common.Attributes;
using Microsoft.Extensions.Logging;
using TTT.Contracts.Interfaces.DependencyInjection;
using TTT.Core.Entities;
using TTT.Database.Contracts.Interfaces.Repositories;
using TTT.Database.Contracts.Models;
using TTT.Server.Items;

namespace TTT.Server.Commands.Admin
{
    public class TestCommands : ISingletonCommand
    {
        private readonly IInventoryItemRepository _itemRepository;
        private readonly ILogger<TestCommands> _logger;

        public TestCommands(IInventoryItemRepository itemRepository, ILogger<TestCommands> logger)
        {
            this._itemRepository = itemRepository;
            this._logger = logger;
        }

        [CustomCommand("testitem")]
        public void SpawnItem(TownPlayer player, string itemName)
        {
            //this._itemRepository.Insert(new InventoryItem
           // {
            //    Quantity = 10,
            //    Type = $"{typeof(FirstAidKit)}",
            //    InventoryId = 1
           // });
        }

        [CustomCommand("getitem")]
        public void SpawnItem(TownPlayer player, int inventoryId)
        {
            //var test = this._itemRepository.Get(i => i.InventoryId == inventoryId).FirstOrDefault();

           // if (test == null) return;

          //  Type itemType = Type.GetType(test.Type);

          //  if (itemType == null) return;

            //object? item = Activator.CreateInstance(itemType, test.InventoryId, test.Quantity);

/*            if (item is itemType)
            {
                
            }*/
        }
    }
}