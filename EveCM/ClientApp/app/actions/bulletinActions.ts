import * as keys from './actionTypeKeys';
import { Dispatch } from 'redux';
import { IBulletin } from '../components/common/bulletins/interfaces/Interfaces';
import * as bulletinConnector from './connectors/bulletinConnector';
import { AxiosResponse } from 'axios';

export interface LoadBulletinSuccessAction {
    type: string,
    bulletins: IBulletin[]
}

export function loadBulletinsSuccess(bulletins: IBulletin[]): LoadBulletinSuccessAction {
    return { type: keys.LOAD_BULLETINS_SUCCESS, bulletins }
}

export function loadBulletins() {
    return function (dispatch: Dispatch) {
        return bulletinConnector.getAllBulletins()
            .then((bulletins: AxiosResponse<IBulletin[]>) => {
                dispatch(loadBulletinsSuccess(bulletins.data));
            })
            .catch(error => { throw (error); });
    }
}