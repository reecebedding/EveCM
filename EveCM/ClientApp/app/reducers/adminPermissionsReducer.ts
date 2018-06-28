import { adminPermissionsState } from './initialState';
import { AdminPermissionsKeys } from '../actions/actionTypeKeys';
import { AdminPermissionsActions } from '../actions/actionTypes';

export function adminPermissions(state = adminPermissionsState.adminPermissions, action: AdminPermissionsActions) {
    switch (action.type) {

        case AdminPermissionsKeys.LOAD_ADMIN_PERMISSIONS_SUCCESS:
            return action.adminPermissions;

        default:
            return state;
    }
}

export function roleInformation(state = adminPermissionsState.roleInformation, action: AdminPermissionsActions) {
    switch (action.type) {

        case AdminPermissionsKeys.LOAD_ROLE_INFORMATION_SUCCESS:
            return action.roleInformation;

        default:
            return state;
    }
}