import * as alt from "alt-client";
import { FlyCam } from "../Admin/Noclip";

export default class ConsoleHandler {
    constructor() {
        alt.on("consoleCommand", this.onConsoleCommand.bind(this));
    }

    public onConsoleCommand(command: string) {
        switch (command) {
            case "pos":
                alt.log(alt.Player.local.pos);
                break;
            case "noclippos":
                FlyCam.getInstance().getInfo();
                break;
            case "noclip":
                FlyCam.getInstance().toggle();
                break;
            default:
                break;
        }
    }
}