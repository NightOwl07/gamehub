using System.Drawing;
using TTT.Core.Contracts.Interfaces.Menu;

namespace TTT.Core.Menu
{
    public class MenuBuilder : IMenuBuilder
    {
        public IMenu Create(string title, string subTitle, Point offset, string spriteLibrary = "",
            string spriteName = "")
        {
            return new Menu(title, subTitle, offset, spriteLibrary, spriteName);
        }
    }
}