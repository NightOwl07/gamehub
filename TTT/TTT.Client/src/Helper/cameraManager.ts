// import * as alt from "alt-client";
// import native from "natives";
// import { Camera } from "./Classes/Camera";

// export default class CameraManager {
//     private static Cameras: Camera[];
//     private static currentCamera: Camera; 
//     private static interpolCamera: Camera; 

//     private static createActiveCamera(position: alt.Vector3, rotation: alt.Vector3): number {
//         native.setFocusPosAndVel(position.x, position.y, position.z, rotation.x, rotation.y, rotation.z);
//         native.setHdArea(position.x, position.y, position.z, 50);

//         let camera: Camera = new Camera();

//         camera.ID = this.Cameras.length + 1;
//         camera.ScriptId = native.createCamWithParams("DEFAULT_SCRIPTED_CAMERA", position.x, position.y, position.z, rotation.x, rotation.y, rotation.z, 80, false, 0);
        
//         native.setCamActive(camera.ScriptId, true);
//         native.renderScriptCams(true, false, 0, false, false, 0);
        
//         this.Cameras.push(camera);

//         return camera.ID;
//     }

//     private static deleteCamera(id: number): void {
//         let camera: Camera = this.Cameras.find(c => c.ID);
//         this.Cameras = this.Cameras.filter(c => c === camera);
//     }
// }