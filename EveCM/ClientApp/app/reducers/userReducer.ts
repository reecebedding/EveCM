import initialState from './initialState';
import * as keys from '../actions/actionTypeKeys';
import { ActionTypes } from '../actions/actionTypes';

export default function userReducer(state = initialState.currentUser, action: ActionTypes) {
    switch (action.type) {

        case keys.LOAD_USER_SUCCESS:
            return action.user;

        default:
            return state;
    }
}