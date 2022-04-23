import IMenuItemData from "./IMenuItemData";
export default interface IMenuItem {
    Text: string;
    Description: string;
    RightLabel: string;
    LowerThreshold: number;
    UpperThreshold: number;
    StartValue: number;
    Checked: boolean;
    CollectionData: any[];
    ItemData: IMenuItemData;
}
