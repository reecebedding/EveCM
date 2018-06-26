import initialState from './initialState';
import * as keys from '../actions/actionTypeKeys';
import { ActionTypes } from '../actions/actionTypes';

export default function bulletins(state = initialState.bulletins, action: ActionTypes) {
    switch (action.type) {

        case keys.LOAD_BULLETINS_SUCCESS:
            return action.bulletins;

        case keys.SAVE_BULLETIN_SUCCESS:
            let newState = [
                Object.assign({}, action.bulletin),
                ...state
            ];
            return newState;

        default:
            return state;
    }
}