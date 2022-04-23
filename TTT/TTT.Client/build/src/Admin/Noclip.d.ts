export declare class FlyCam {
    private static _instance;
    private _camId;
    private _tickId;
    private _maxSpeed;
    private _minSpeed;
    private _speed;
    isActive: boolean;
    private _currentRot;
    private constructor();
    static getInstance(): FlyCam;
    toggle(): void;
    private handleCamera;
    private clamp;
    private rotationToDirection;
    private getNormalizedVector;
    private getCrossProduct;
    getInfo(): void;
}
