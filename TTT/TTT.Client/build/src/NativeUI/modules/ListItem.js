import UUIDV4 from "../utils/UUIDV4";
export default class ListItem {
    Id = UUIDV4();
    DisplayText;
    Data;
    constructor(text = "", data = null) {
        this.DisplayText = text;
        this.Data = data;
    }
}
