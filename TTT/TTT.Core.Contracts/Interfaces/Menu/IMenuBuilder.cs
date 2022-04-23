using System.Drawing;

namespace TTT.Core.Contracts.Interfaces.Menu
{
    public interface IMenuBuilder
    {
        IMenu Create(string title, string subTitle, Point offset, string spriteLibrary = "", string spriteName = "");
    }
}