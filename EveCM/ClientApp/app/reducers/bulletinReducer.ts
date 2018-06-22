import initialState from './initialState';
import * as keys from '../actions/actionTypeKeys';
import { ActionTypes } from '../actions/actionTypes';

export default function bulletinReducer(state = initialState.bulletins, action: ActionTypes) {
    switch (action.type) {

        case keys.LOAD_BULLETINS_SUCCESS:
            return action.bulletins;

        default:
            return state;
    }
}