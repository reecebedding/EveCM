import { bulletinState } from './initialState';
import { UserKeys } from '../actions/actionTypeKeys';
import { UserActions } from '../actions/actionTypes';

export default function userReducer(state = bulletinState.currentUser, action: UserActions) {
    switch (action.type) {

        case UserKeys.LOAD_USER_SUCCESS:
            return action.user;

        default:
            return state;
    }
}