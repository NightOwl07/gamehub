import game from 'natives';
export default class InstructionalButton {
    Text;
    get ItemBind() { return this._itemBind; }
    _itemBind = null;
    _buttonString;
    _buttonControl;
    _usingControls;
    constructor(text, control, buttonString = null) {
        this.Text = text;
        this._buttonControl = control;
        this._usingControls = buttonString == null;
        this._buttonString = buttonString;
    }
    BindToItem(item) {
        this._itemBind = item;
    }
    GetButtonId() {
        return this._usingControls ? game.getControlInstructionalButton(2, this._buttonControl, false) : "t_" + this._buttonString;
    }
}
