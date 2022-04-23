import React from "react";
import "./alt/alt";
import { alt } from "./alt/alt";
import WelcomeForm from "./Modules/AuthModule/WelcomeForm";
import InventoryModule from "./Modules/InventoryModule/InventoryModule";
import NotificationModule from "./Modules/NotificationModule/NotificationModule";

export interface MainProps {
}

interface MainState {
    modules: string[];
}

interface IPageableComponent {
    name: string;
    component: React.ComponentType;
}

const Components: IPageableComponent[] = [
    { name: "welcome", component: WelcomeForm },
    { name: "inventory", component: InventoryModule },
    { name: "notification", component: NotificationModule }
]

/**
 * Component Main
 *
 * @export
 * @class Main
 * @extends {React.Component<MainProps, MainState>}
 */
export default class Main extends React.Component<MainProps, MainState> {
    constructor(props: MainProps) {
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
