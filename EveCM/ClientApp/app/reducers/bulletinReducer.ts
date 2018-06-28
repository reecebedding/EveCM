import { bulletinState } from './initialState';
import { BulletinKeys } from '../actions/actionTypeKeys';
import { BulletinActions } from '../actions/actionTypes';

export default function bulletins(state = bulletinState.bulletins, action: BulletinActions) {
    switch (action.type) {

        case BulletinKeys.LOAD_BULLETINS_SUCCESS:
            return action.bulletins;

        case BulletinKeys.SAVE_BULLETIN_SUCCESS:
            return [
                action.bulletin,
                ...state
            ];

        default:
            return state;
    }
}