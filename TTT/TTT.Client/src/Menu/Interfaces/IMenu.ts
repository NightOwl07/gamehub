import IMenuItem from "./IMenuItem";

export default interface IMenu {
    Id: string;
    Title: string,
    SubTitle: string,
    Offset: number[],
    SpriteLibrary: string,
    SpriteName: string,
    Items: IMenuItem[]
}