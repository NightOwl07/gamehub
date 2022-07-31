import { alt } from "./alt/alt";
import React from "react";
import WelcomeForm from "./Modules/AuthModule/WelcomeForm";
import NotificationModule from "./Modules/NotificationModule/NotificationModule";

export interface IndexProps {
}

interface IndexState {
    modules: string[];
}

interface IPageableComponent {
    name: string;
    component: React.ComponentType;
}

const Components: IPageableComponent[] = [
    { name: "welcome", component: WelcomeForm },
    { name: "notification", component: NotificationModule }
]

/**
 * Component Index
 *
 * @export
 * @class Index
 * @extends {React.Component<IndexProps, IndexState>}
 */
export default class Index extends React.Component<IndexProps, IndexState> {
    constructor(props: IndexProps) {
        super(props);
        this.state = {
            modules: ["notification"],
        };
    }

    public render(): JSX.Element {
        return (
            <>
                {this.state.modules.map((moduleName) => {
                    let Module = Components.find((m) => m.name === moduleName)?.component;
                    if (!Module) {
                        console.log("Module can't be resolved");
                        return null;
                    }

                    return (
                        <Module key={moduleName} />
                    );
                })}
            </>
        );
    }

    public componentDidMount() {
        alt.on("TTT:View:ShowModule", this.onShowModule.bind(this));
        alt.on("TTT:View:HideModule", this.onHideModule.bind(this));
        alt.emit("TTT:View:Ready");
    }

    private onShowModule(module: string) {
        console.log("new module", module);
        this.setState(prevState => ({ modules: [...prevState.modules, module] }));
    }

    private onHideModule(module: string) {
        console.log("hide module", module);
        this.setState(prevState => ({ modules: prevState.modules.filter(m => m !== module) }));
    }
}
