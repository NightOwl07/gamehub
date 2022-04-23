import * as alt from 'alt-client';
import Scaleform from '../utils/Scaleform';
export default class Message {
    static _messageVisible = false;
    static _transitionOutTimeout = null;
    static _transitionOutFinishedTimeout = null;
    static _delayedTransitionInTimeout = null;
    static _scaleform = null;
    static _transitionOutTimeMs = 500;
    static _transitionOutAnimName = null;
    static Initialize(scaleForm, transitionOutAnimName) {
        this._transitionOutAnimName = transitionOutAnimName;
        this._scaleform = new Scaleform(scaleForm);
    }
    static get IsVisible() {
        return this._messageVisible;
    }
    static get Scaleform() {
        return this._scaleform;
    }
    static Load() {
        if (this._delayedTransitionInTimeout != null) {
            alt.clearTimeout(this._delayedTransitionInTimeout);
            this._delayedTransitionInTimeout = null;
        }
    }
    static SetDelayedTransition(messageHandler, time) {
        this._delayedTransitionInTimeout = alt.setTimeout(() => {
            this._delayedTransitionInTimeout = null;
            this.TransitionIn(messageHandler, time);
        }, this._transitionOutTimeMs);
    }
    static ShowCustomShard(funcName, time = 5000, ...funcArgs) {
        this.ShowComplexCustomShard(() => {
            this._scaleform.callFunction(funcName, ...funcArgs);
        }, time);
    }
    static ShowComplexCustomShard(messageHandler, time = 5000) {
        this.Load();
        if (this._messageVisible) {
            this.TransitionOut();
            this.SetDelayedTransition(() => messageHandler(), time);
        }
        else {
            this.TransitionIn(messageHandler, time);
        }
    }
    static TransitionOut() {
        if (!this._messageVisible)
            return;
        if (this._transitionOutTimeout != null) {
            alt.clearTimeout(this._transitionOutTimeout);
            this._transitionOutTimeout = null;
        }
        if (this._transitionOutFinishedTimeout != null) {
            alt.clearTimeout(this._transitionOutFinishedTimeout);
            this._transitionOutFinishedTimeout = null;
        }
        this._scaleform.callFunction(this._transitionOutAnimName);
        this._transitionOutFinishedTimeout = alt.setTimeout(() => {
            this._messageVisible = false;
            this._scaleform.recreate();
        }, this._transitionOutTimeMs);
    }
    static TransitionIn(messageHandler, transitionOutTime = 500) {
        this._messageVisible = true;
        messageHandler();
        this.SetTransitionOutTimer(transitionOutTime);
    }
    static SetTransitionOutTimer(time) {
        this._transitionOutTimeout = alt.setTimeout(() => {
            this._transitionOutTimeout = null;
            this.TransitionOut();
        }, time);
    }
    static Render() {
        if (this._messageVisible) {
            this._scaleform.render2D();
        }
    }
}
