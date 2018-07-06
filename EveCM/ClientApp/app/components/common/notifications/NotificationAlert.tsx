import * as React from 'react';
//@ts-ignore
import { NotificationContainer, NotificationManager } from 'react-notifications';

export enum NotificationType {
    Success,
    Info,
    Warning,
    Error
}

export class NotificationAlertContainer extends React.Component {
    render() {
        return (
            <NotificationContainer />
        );
    }
}

export class NotificationAlertManager {
    public static alert(type: NotificationType, body: string, title: string = '', timeout: number = 0) {
        switch (type) {
            case NotificationType.Success:
                NotificationManager.success(body, title, timeout);
                break;
            case NotificationType.Info:
                NotificationManager.info(body, title, timeout);
                break;
            case NotificationType.Warning:
                NotificationManager.warning(body, title, timeout);
                break;
            case NotificationType.Error:
                NotificationManager.error(body, title, timeout);
                break;

            default:
        }
    }
}