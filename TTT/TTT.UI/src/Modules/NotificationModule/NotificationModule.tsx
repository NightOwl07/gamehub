import { BaseModule } from "../../Base/BaseModule";
import { alt } from '../../alt/alt';
import { NotificationType } from "./Enums/NotificationType";
import "./NotificationModule.css"

interface INotification {
    title: string;
    message: string;
    type: NotificationType;
    duration: number;
    animate: boolean;
}

interface NotificationModuleProps { }

interface NotificationModuleState {
    notifications: INotification[];
}

export default class NotificationModule extends BaseModule<NotificationModuleProps, NotificationModuleState> {
    public moduleName: string;

    constructor(props: NotificationModuleProps) {
        super(props);

        this.moduleName = "notifications";

        this.state = {
            notifications: []
        }
    }

    public render(): JSX.Element {
        return (
            <>
                <div className="absolute m-1 top-0 left-0 w-1/4">
                    {this.state.notifications.length > 0 && this.state.notifications.map((notification, i) => {
                        let animateClass = notification.animate ? `animation--${Math.random() * 10 <= 5 ? "shake" : "boom"}` : '';
                        
                        notification.animate = false;

                        return (
                            <div key={`${Math.random() * 10 + i}`} className={`alert mb-1 ${animateClass}`}>
                                <div className="flex-1">
                                    <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke="#009688" className="flex-shrink-0 w-6 h-6 mx-2">
                                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M15 17h5l-1.405-1.405A2.032 2.032 0 0118 14.158V11a6.002 6.002 0 00-4-5.659V5a2 2 0 10-4 0v.341C7.67 6.165 6 8.388 6 11v3.159c0 .538-.214 1.055-.595 1.436L4 17h5m6 0v1a3 3 0 11-6 0v-1m6 0H9"></path>
                                    </svg>
                                    <label>
                                        <h4>{notification.title}</h4>
                                        <p className="text-sm text-base-content text-opacity-60">
                                            {notification.message}
                                        </p>
                                    </label>
                                </div>
                            </div>
                        )
                    })}
                </div>
            </>
        );
    }

    public moduleInit() {
        alt.on("TTT:NotificationView:NewNotification", this.newNotification.bind(this));
    }

    public newNotification(type: number, title: string, message: string, duration: number) {
        let inNotification: INotification = {
            title: title,
            message: message,
            duration: duration,
            type: type,
            animate: true
        }

        this.setState(prevState => ({ notifications: [inNotification, ...prevState.notifications] }));

        setTimeout(() => {
            this.setState(prevState => ({ notifications: prevState.notifications.filter(n => n !== inNotification) }));
        }, inNotification.duration);
    }
}

