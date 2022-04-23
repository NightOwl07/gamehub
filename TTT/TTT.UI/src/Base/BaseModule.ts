import React from 'react';
import { alt } from '../alt/alt';

export abstract class BaseModule<P = {}, S = {}, SS = {}> extends React.Component<P, S, SS> {
    abstract moduleName: string;

    abstract moduleInit(): void;

    public componentDidMount() {
        this.moduleInit();

        alt.emit("TTT:View:ModuleMounted", this.moduleName);
    }

    public componentWillUnmount() {
        alt.emit("TTT:View:ModuleUnmouted", this.moduleName);
    }
}