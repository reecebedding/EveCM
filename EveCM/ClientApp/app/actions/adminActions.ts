import { Dispatch, Action } from "redux";
import { AxiosResponse } from "axios";
import { IAdminPermissions, IRoleInformation } from './connectors/admin/Interfaces';
import * as PermissionSettingsConnector from './connectors/admin/permissionSettingsConnector';

import { AdminPermissionsKeys } from './actionTypeKeys';
import { IUserInRole } from "../components/Admin/permissions/interfaces/Interfaces";

export interface LoadAdminPermissionSuccessAction extends Action {
    adminPermissions: IAdminPermissions
}
export function loadAdminPermissionSuccess(adminPermissions: IAdminPermissions): LoadAdminPermissionSuccessAction {
    return { type: AdminPermissionsKeys.LOAD_ADMIN_PERMISSIONS_SUCCESS, adminPermissions }
}

export interface LoadRoleInformationSuccessAction extends Action {
    roleInformation: IRoleInformation
}

export function loadRoleInformationSuccess(roleInformation: IRoleInformation): LoadRoleInformationSuccessAction {
    return { type: AdminPermissionsKeys.LOAD_ROLE_INFORMATION_SUCCESS, roleInformation}
}

export interface RemoveMemberFromRoleSuccessAction extends Action {
    roleName: string,
    user: IUserInRole
}

export function removeMemberFromRoleSuccess(user: IUserInRole, roleName: string): RemoveMemberFromRoleSuccessAction {
    return { type: AdminPermissionsKeys.REMOVE_MEMBER_FROM_ROLE_SUCCESS, roleName, user }
}

export interface AddMemberToRoleSuccessAction extends Action {
    roleName: string,
    user: IUserInRole
}

export function addMemberToRoleSuccess(user: IUserInRole, roleName: string): AddMemberToRoleSuccessAction {
    return { type: AdminPermissionsKeys.ADD_MEMBER_TO_ROLE_SUCCESS, roleName, user }
}

export function dismissRemoveMemberFromRoleSuccess(): Action {
    return { type: AdminPermissionsKeys.DISMISS_REMOVE_MEMBER_FROM_ROLE_SUCCESS };
}

export function dismissAddMemberToRoleSuccess(): Action {
    return { type: AdminPermissionsKeys.DISMISS_ADD_MEMBER_TO_ROLE_SUCCESS };
}

export function loadAdminPermissions() {
    return function (dispatch: Dispatch) {
        return PermissionSettingsConnector.getPermissionSettings()
            .then((permissions: AxiosResponse<IAdminPermissions>) => {
                dispatch(loadAdminPermissionSuccess(permissions.data));
            })
            .catch(error => { throw (error); });
    }
}

export function loadRoleInformation(roleName: string) {
    return function (dispatch: Dispatch) {
        return PermissionSettingsConnector.getRoleInformation(roleName)
            .then((roleInformation: AxiosResponse<IRoleInformation>) => {
                dispatch(loadRoleInformationSuccess(roleInformation.data));
            })
            .catch(error => { throw (error); });
    }
}

export function removeMemberFromRole(user: IUserInRole, role: string) {
    return function (dispatch: Dispatch) {
        return PermissionSettingsConnector.removeUserFromRole(user, role)
            .then((userRemove: AxiosResponse<IUserInRole>) => {
                dispatch(removeMemberFromRoleSuccess(userRemove.data, role));
            })
            .catch(error => { throw (error); });
    }
}

export function addMemberToRole(user: IUserInRole, role: string) {
    return function (dispatch: Dispatch) {
        return PermissionSettingsConnector.addUserToRole(user, role)
            .then((userAdded: AxiosResponse<IUserInRole>) => {
                dispatch(addMemberToRoleSuccess(userAdded.data, role));
            })
            .catch(error => { throw (error); });
    }
}