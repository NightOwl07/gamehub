import { BaseModule } from '../../Base/BaseModule';
import { AuthState } from "./Enums/AuthState";
import RegisterForm from "./RegisterForm";
import LoginForm from "./LoginForm";
import './AuthModule.css';

interface AuthModuleProps { }

interface AuthModuleState {
    authState: AuthState;
}

export default class WelcomeForm extends BaseModule<AuthModuleProps, AuthModuleState> {
    public moduleName: string;

    constructor(props: AuthModuleProps) {
        super(props);

        this.moduleName = "welcome";

        this.state = {
            authState: AuthState.Welcome
        }
    }

    public render(): JSX.Element {
        return (
            <div className="flex items-center justify-center h-screen">
                {this.state.authState === AuthState.Welcome &&
                    <div className="logincard card card-side bg-base-100 shadow-xl bordered w-1/2">
                        <figure>
                            <img alt="just a cat" style={{ maxWidth: "350px", maxHeight: "100%" }} src="https://images.unsplash.com/photo-1568307970720-a8c50b644a7c?ixid=MnwxMjA3fDB8MHxzZWFyY2h8MXx8c2NhcmVkJTIwY2F0fGVufDB8fDB8fA%3D%3D&ixlib=rb-1.2.1&w=1000&q=80" />
                        </figure>
                        <div className="card-body">
                            <h2 className="card-title">Willkommen auf TTT</h2>
                            <p>Was möchtest du tun? Rechts eventuell News anzeigen lassen? Idk.</p>
                            <p>Du hurensohn.</p>
                            <div className="card-actions">
                                <button onClick={this.onLoginButtonClick.bind(this)} className="btn btn-primary">Login</button>
                                <button onClick={this.onRegisterButtonClick.bind(this)} className="btn btn-ghost">Register</button>
                            </div>
                        </div>
                    </div>
                }
                {this.state.authState === AuthState.Register &&
                    <RegisterForm onNavigateBack={this.onNavigateToWelcomeForm.bind(this)} onNavigateForwardParent={this.onNavigateForward.bind(this)}></RegisterForm>
                }
                {this.state.authState === AuthState.Login &&
                    <LoginForm onLogin={this.onLogin.bind(this)}></LoginForm>
                }
            </div>
        );
    }

    public onLoginButtonClick() {
        this.setState({ authState: AuthState.Login });
    }

    public onRegisterButtonClick() {
        this.setState({ authState: AuthState.Register });
    }

    public onNavigateForward() {
        if (this.state.authState === AuthState.Register) {
            this.setState({ authState: AuthState.Login }); // after time, replace with "characterselection"
        }
        else {
            this.setState({ authState: AuthState.Welcome });
        }
    }

    public onNavigateToWelcomeForm() {
        this.setState({ authState: AuthState.Welcome });
    }

    public onLogin() {

    }
}

