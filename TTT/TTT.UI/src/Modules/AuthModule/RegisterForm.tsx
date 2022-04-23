import React from 'react';
import { alt } from '../../alt/alt';
import './AuthModule.css';

interface IAccount {
    username: string;
    email: string;
    password: string;
}

interface RegisterFormProps {
    onNavigateBack(): void;
    onNavigateForwardParent(): void;
}

interface RegisterFormState {
    account: IAccount;
    username: string;
    email: string;
    password: string;
    passwordrepeat: string;
    errorMessage: string;
    isLoading: boolean;
}

export default class RegisterForm extends React.Component<RegisterFormProps, RegisterFormState> {
    constructor(props: RegisterFormProps) {
        super(props);

        this.state = {
            account: {
                username: "",
                email: "",
                password: ""
            },
            username: "",
            email: "",
            password: "",
            passwordrepeat: "",
            errorMessage: "",
            isLoading: true
        }
    }

    public render(): JSX.Element {
        return (
            <div className="flex items-center justify-center h-screen">
                <div className="login card bordered w-96">
                    <div className="card-body">
                        <h2 className="card-title">Registrierung</h2>

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
                                <input type="text" placeholder="Username" onChange={(e) => this.setState({ username: e.target.value })} className="input" />

                                <label className="label">
                                    <span className="label-text">Email</span>
                                </label>
                                <input type="email" placeholder="Email" onChange={(e) => this.setState({ email: e.target.value })} className="input" />

                                <label className="label">
                                    <span className="label-text">Passwort</span>
                                </label>
                                <input type="password" placeholder="Passwort" onChange={this.onPasswordUnfocused.bind(this)} className="input" />

                                <label className="label">
                                    <span className="label-text">Passwort (wdh.)</span>
                                </label>
                                <input type="password" placeholder="Passwort (wdh.)" onBlur={this.onCheckPasswordRepeat.bind(this)} className="input" />

                                <div className="card-actions w-full items-end">
                                    <button onClick={this.onNavigateForward.bind(this)} className="btn btn-primary">Weiter</button>
                                    <button onClick={this.props.onNavigateBack} className="btn btn-ghost">Zurück</button>
                                </div>
                            </>
                        }
                    </div>
                </div>
            </div>
        );
    }

    public componentDidMount() {
        this.setState({ isLoading: false });
    }

    public onNavigateForward() {
        if (this.state.errorMessage !== "") {
            return;
        }

        if (this.state.email === "" || this.state.username === "" || this.state.password === "" || this.state.password !== this.state.passwordrepeat) {
            this.setState({ errorMessage: "Es müssen alle Felder korrekt ausgefüllt sein!" });
            return;
        }

        this.setState({ isLoading: true });

        // this.props.onNavigateForwardParent();
        alt.emit("TTT:RegisterView:RegisterComplete", this.state.email, this.state.username, this.state.password);
    }

    public onPasswordUnfocused(e: any) {
        if (e.target.value.length < 8) {
            this.setState({ errorMessage: "Passwort muss mindestens 8 Zeichen lang sein!" });
            return;
        }

        this.setState({ password: e.target.value, errorMessage: "" });
    }

    public onCheckPasswordRepeat(e: any) {
        if (this.state.password !== e.target.value) {
            this.setState({ errorMessage: "Passwörter stimmen nicht überein!" });
            return;
        }

        this.setState({ passwordrepeat: e.target.value, errorMessage: "" });
    }
}

