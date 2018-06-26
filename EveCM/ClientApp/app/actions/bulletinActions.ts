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
    return { type: keys.LOAD_BULLETINS_SUCCESS, bulletins };
}

export interface SaveBulletinSuccessAction {
    type: string,
    bulletin: IBulletin
}
export function saveBulletinSuccess(bulletin: IBulletin): SaveBulletinSuccessAction {
    return { type: keys.SAVE_BULLETIN_SUCCESS, bulletin };
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

export function saveBulletin(bulletin: IBulletin) {
    return function (dispatch: Dispatch) {
        return bulletinConnector.saveNewBulletin(bulletin)
            .then((bulletin: AxiosResponse<IBulletin>) => {
                dispatch(saveBulletinSuccess(bulletin.data));
            })
            .catch(error => { throw (error); });
    }
}