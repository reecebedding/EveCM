import { Dispatch, Action } from "redux";
import { AxiosResponse } from "axios";
import { IAdminPermissions, IRoleInformation } from './connectors/admin/Interfaces';
import * as PermissionSettingsConnector from './connectors/admin/permissionSettingsConnector';

import { AdminPermissionsKeys } from './actionTypeKeys';

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