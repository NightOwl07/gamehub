namespace TTT.Core.Contracts.Interfaces.Menu.Items
{
    public interface IUIMenuItem
    {
        IMenuItemData ItemData { get; }

        void ExecuteAction(string result);
    }
}