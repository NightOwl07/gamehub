import * as alt from "alt-client";
import native from "natives";
import AccountHandler from "./Handler/accountHandler";
import CharacterHandler from "./Handler/characterHandler";
import ConsoleHandler from "./Handler/consoleHandler";
import MenuHandler from "./Handler/menuHandler";
import NotificationHandler from "./Handler/notificationHandler";
import VehicleHandler from "./Handler/vehicleHandler";
import TextLabelManager from "./Helper/textLabelManager";
import Utils from "./Utils/utils";
import Browser from "./WebView/Browser";

class TTTClient {
    public notificationService: NotificationHandler;
    private browserInstance: alt.WebView;

    constructor() {
        alt.everyTick(this.onEveryTick.bind(this));
    }

    public SetupAsync(): Promise<void> {
        return new Promise(async (resolve, reject) => {
            this.browserInstance = await Browser.getInstance();

            this.setupLoginScene();

            if (!this.browserInstance) {
                reject();
            }

            new AccountHandler(this.browserInstance);
            new ConsoleHandler();
            new CharacterHandler();
            new MenuHandler();
            new VehicleHandler();
            new TextLabelManager();

            this.notificationService = new NotificationHandler(this.browserInstance);

            alt.onServer("TTT:Utils:SetPedIntoVehicle", Utils.setPedIntoVehicle);
            alt.onServer("TTT:Utils:SetVehicleEngineMultiplier", Utils.setVehicleEngineMultiplier);

            resolve();
        });
    }

    private onEveryTick(): void {
        native.setPedConfigFlag(alt.Player.local, 241, true);
        native.setPedConfigFlag(alt.Player.local, 429, true);
        native.setPedConfigFlag(alt.Player.local, 35, true);
    }

    private setupLoginScene(): void {
        native.freezeEntityPosition(alt.Player.local.scriptID, true);
        native.setEntityCoords(alt.Player.local.scriptID, 866.9821, 1256.2087, 401.9878, false, false, false, false);
        let camId: number = native.createCamWithParams("DEFAULT_SCRIPTED_CAMERA", 866.9821, 1256.2087, 451.9878, 0, 0, 0, 80, false, 0);
        native.setCamActive(camId, true);
        native.setCamRot(camId, -23.0710, 0.0000, 154.3693, 2);
        native.renderScriptCams(true, false, 0, true, false, 0);
        native.setCamAffectsAiming(camId, false);
        native.setClockTime(20, 0, 0);
        native.pauseClock(true);
        native.setWeatherTypeNow("CLEAR");
        native.displayRadar(false);
        native.displayAreaName(false);

        alt.beginScaleformMovieMethodMinimap('SETUP_HEALTH_ARMOUR');
        native.scaleformMovieMethodAddParamInt(3);
        native.endScaleformMovieMethod();

        native.replaceHudColourWithRgba(143, 80, 64, 255, 255);
        native.replaceHudColourWithRgba(116, 80, 64, 255, 255);
    }
}

export const Client: TTTClient = new TTTClient();