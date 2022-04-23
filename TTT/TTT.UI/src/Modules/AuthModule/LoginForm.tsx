import React from 'react';
import { alt } from '../../alt/alt';
import './AuthModule.css';

interface LoginFormProps {
    onLogin(username: string, password: string): void;
}

interface LoginFormState {
    username: string;
    password: string;
    errorMessage: string;
    isLoading: boolean;
}

export default class LoginForm extends React.Component<LoginFormProps, LoginFormState> {
    constructor(props: LoginFormProps) {
        super(props);

        this.state = {
            username: "",
            password: "",
            errorMessage: "",
            isLoading: true
        }
    }

    public render(): JSX.Element {
        return (
            <div className="flex items-center justify-center h-screen">
                <div className="login card bordered w-96">
                    <div className="card-body">
                        <h2 className="card-title">Login</h2>

                        {this.state.errorMessage &&
                            <div className="alert alert-error">
                                <div className="flex-1">
                                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" className="w-6 h-6 mx-2 stroke-current">
                                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M18.364 18.364A9 9 0 005.636 5.636m12.728 12.728A9 9 0 015.636 5.636m12.728 12.728L5.636 5.636"></path>
                                    </svg>
                                    <label>{this.state.errorMessage}</label>
                                </div>
                            </div>
                        }

                        {this.state.isLoading &&
                            <div className="pulse-container">
                                <div className="pulse-bubble pulse-bubble-1"></div>
                                <div className="pulse-bubble pulse-bubble-2"></div>
                                <div className="pulse-bubble pulse-bubble-3"></div>
                            </div>
                        }

                        {!this.state.isLoading &&
                            <>
                                <label className="label">
                                    <span className="label-text">Username</span>
                                </label>
                                <input type="text" placeholder="Username o. Email" onBlur={(e) => this.setState({ username: e.target.value })} className="input" />

                                <label className="label">
                                    <span className="label-text">Passwort</span>
                                </label>
                                <input type="password" placeholder="Passwort" onBlur={(e) => this.setState({ password: e.target.value })} className="input" />

                                <div className="card-actions w-full items-end">
                                    <button onClick={this.onLogin.bind(this)} className="btn btn-primary">Weiter</button>
                                </div>
                            </>
                        }

                    </div>
                </div>
            </div>
        );
    }

    public componentDidMount() {
        alt.on("TTT:LoginView:StopLoading", this.onStopLoading.bind(this));
        this.setState({ isLoading: false });
    }

    public onLogin() {
        if (this.state.password === "" || this.state.username === "") {
            this.setState({ errorMessage: "Alle Felder müssen korrekt ausgefüllt sein!" });
            return;
        }

        this.setState({ isLoading: true, errorMessage: "" });

        alt.emit("TTT:LoginView:LoginComplete", this.state.username, this.state.password);
    }

    public onStopLoading() {
        this.setState({ isLoading: false });
    }
}

