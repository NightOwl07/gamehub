namespace TTT.Server.Contracts.Interfaces.Items
{
    public interface IItem
    {
        public int InventoryId { get; set; }

        public string Name { get; }

        public string Description { get; }
    }
}