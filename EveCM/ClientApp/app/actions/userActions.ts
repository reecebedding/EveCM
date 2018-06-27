import * as keys from './actionTypeKeys';
import { Dispatch, Action } from 'redux';
import { IUser } from '../components/common/userDetails/interfaces/Interfaces';
import * as userConnector from './connectors/userConnector';
import { AxiosResponse } from 'axios';

export interface LoadUserSuccessAction extends Action<string>{
    user: IUser
}

export function loadUserSuccess(user: IUser): LoadUserSuccessAction {
    return { type: keys.LOAD_USER_SUCCESS, user }
}

export function loadCurrentUser() {
    return function (dispatch: Dispatch) {
        return userConnector.getCurrentUser()
            .then((user: AxiosResponse<IUser>) => {
                dispatch(loadUserSuccess(user.data));
            })
            .catch(error => { throw (error); });
    }
}