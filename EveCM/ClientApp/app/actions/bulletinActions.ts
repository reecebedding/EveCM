import { BulletinKeys } from './actionTypeKeys';
import { Dispatch, Action } from 'redux';
import { IBulletin } from '../components/common/bulletins/interfaces/Interfaces';
import * as bulletinConnector from './connectors/bulletinConnector';
import { AxiosResponse } from 'axios';

export interface LoadBulletinSuccessAction extends Action<string>{
    bulletins: IBulletin[]
}

export function loadBulletinsSuccess(bulletins: IBulletin[]): LoadBulletinSuccessAction {
    return { type: BulletinKeys.LOAD_BULLETINS_SUCCESS, bulletins };
}

export interface SaveBulletinSuccessAction extends Action<string> {
    bulletin: IBulletin
}

export function saveBulletinSuccess(bulletin: IBulletin): SaveBulletinSuccessAction {
    return { type: BulletinKeys.SAVE_BULLETIN_SUCCESS, bulletin };
}

export interface RemoveBulletinSuccessAction extends Action<string> {
    bulletin: IBulletin
}

export function removeBulletinSuccess(bulletin: IBulletin): RemoveBulletinSuccessAction {
    return { type: BulletinKeys.REMOVE_BULLETIN_SUCCESS, bulletin };
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

export function removeBulletin(bulletin: IBulletin) {
    return function (dispatch: Dispatch) {
        return bulletinConnector.removeBulletin(bulletin)
            .then((bulletin: AxiosResponse<IBulletin>) => {
                dispatch(removeBulletinSuccess(bulletin.data));
            })
            .catch(error => { throw (error); });
    }
}