﻿import { adminPermissionsState } from './initialState';
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
            return {
                ...state,
                data: {
                    ...state.data,
                    ...action.roleInformation
                }
            };

        case AdminPermissionsKeys.REMOVE_MEMBER_FROM_ROLE_SUCCESS: {
            if (state.data.name == action.roleName) {
                return {
                    ...state,
                    data: {
                        ...state.data,
                        users: state.data.users.filter(u => u.id !== action.user.id),
                        usersToAdd: [
                            ...state.data.usersToAdd,
                            action.user
                        ]
                    },
                    ui: {
                        ...state.ui,
                        userRemoved: true
                    }
                };
            } else {
                return state;
            }
        }

        case AdminPermissionsKeys.ADD_MEMBER_TO_ROLE_SUCCESS: {
            if (state.data.name == action.roleName) {
                return {
                    ...state,
                    data: {
                        ...state.data,
                        users: [
                            ...state.data.users,
                            action.user
                        ],
                        usersToAdd: state.data.usersToAdd.filter(u => u.id !== action.user.id)
                    },
                    ui: {
                        ...state.ui,
                        userAdded: true
                    }
                };
            } else {
                return state;
            }
        }

        case AdminPermissionsKeys.DISMISS_REMOVE_MEMBER_FROM_ROLE_SUCCESS:
            return {
                ...state,
                ui: {
                    ...state.ui,
                    userRemoved: false
                }
            };

        case AdminPermissionsKeys.DISMISS_ADD_MEMBER_TO_ROLE_SUCCESS:
            return {
                ...state,
                ui: {
                    ...state.ui,
                    userAdded: false
                }
            };

        default:
            return state;
    }
}