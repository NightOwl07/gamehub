import * as alt from 'alt-client';
import * as NativeUI from "../NativeUI/NativeUi";
import { MenuItemType } from '../Menu/Enums/MenuItemType';
export default class MenuHandler {
    dynamicListSelectionIndex;
    dynamicListSelection;
    constructor() {
        this.dynamicListSelectionIndex = 0;
        this.dynamicListSelection = null;
        alt.onServer("TTT:MenuHandler:CreateNewMenu", this.onCreateNewMenu.bind(this));
    }
    onCreateNewMenu(menuJson) {
        let menu = JSON.parse(menuJson);
        let nativeMenu = new NativeUI.Menu(menu.Title, menu.SubTitle, new NativeUI.Point(50, 50), menu.SpriteLibrary, menu.SpriteName);
        this.createMenuItems(nativeMenu, menu.Items);
        nativeMenu.ItemSelect.on(item => {
            alt.emitServer("TTT:MenuHandler:MenuInteraction", item.Data, this.dynamicListSelection);
        });
        nativeMenu.MenuClose.on((menu) => {
            alt.emitServer("TTT:MenuHandler:MenuClose");
        });
        nativeMenu.Visible = true;
        nativeMenu.Open();
    }
    createMenuItems(nativeMenu, items) {
        items.forEach(item => {
            let itemToAdd = undefined;
            switch (item.ItemData.ItemType) {
                case MenuItemType.AutoListItem:
                    itemToAdd = new NativeUI.UIMenuAutoListItem(item.Text, item.Description, item.LowerThreshold, item.UpperThreshold, item.StartValue);
                    break;
                case MenuItemType.CheckboxItem:
                    itemToAdd = new NativeUI.UIMenuCheckboxItem(item.Text, item.Checked, item.Description);
                    break;
                case MenuItemType.DynamicListItem:
                    itemToAdd = new NativeUI.UIMenuDynamicListItem(item.Text, (listItem, selectedValue, changeDirection) => {
                        return this.onSelectionChanged(listItem, selectedValue, changeDirection, item.CollectionData);
                    }, item.Description, () => {
                        return this.onSelectedStartValueHandler(item.CollectionData);
                    }, item.ItemData);
                    break;
                case MenuItemType.Item:
                    itemToAdd = new NativeUI.UIMenuItem(item.Text, item.Description);
                    itemToAdd.RightLabel = item.RightLabel;
                    break;
                case MenuItemType.ListItem:
                    itemToAdd = new NativeUI.UIMenuListItem(item.Text, item.Description);
                    break;
                case MenuItemType.SliderItem:
                    itemToAdd = new NativeUI.UIMenuSliderItem(item.Text, item.CollectionData, 0, item.Description, true, item.ItemData);
                    break;
                default:
                    break;
            }
            itemToAdd.Data = JSON.stringify(item.ItemData);
            nativeMenu.AddItem(itemToAdd);
        });
    }
    onSelectedStartValueHandler(collectionData) {
        this.dynamicListSelection = collectionData[0];
        return collectionData[0];
    }
    onSelectionChanged(item, selectedValue, changeDirection, collectionData) {
        if (changeDirection == NativeUI.ChangeDirection.Right) {
            this.dynamicListSelectionIndex++;
            if (this.dynamicListSelectionIndex >= collectionData.length)
                this.dynamicListSelectionIndex = 0;
        }
        else {
            this.dynamicListSelectionIndex--;
            if (this.dynamicListSelectionIndex < 0)
                this.dynamicListSelectionIndex = collectionData.length - 1;
        }
        this.dynamicListSelection = collectionData[this.dynamicListSelectionIndex];
        return collectionData[this.dynamicListSelectionIndex];
    }
}
