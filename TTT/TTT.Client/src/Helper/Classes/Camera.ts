// import * as alt from "alt-client";
// import native from "natives";

// export class Camera {
//     public ID: number;
//     public ScriptId: number;

//     constructor(id?: number, scriptId?: number) {
//         this.ID = id || -1;
//         this.ScriptId = scriptId || -1;
//     }

//     public Destory(): void {
//         native.renderScriptCams(false, false, 0, true, false, 0);
//         native.destroyCam(this.ScriptId, true);
//         native.destroyAllCams(true);
//         native.setFollowPedCamViewMode(1);
//         native.clearFocus();
//     }
// }