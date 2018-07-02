import axios from 'axios';
import { IUserInRole } from '../../../components/Admin/permissions/interfaces/Interfaces';

axios.defaults.headers.put["Content-Type"] = 'application/json';
export function getPermissionSettings() {
    return axios.get('/api/admin/permissions');
}

export function getRoleInformation(roleName: string) {
    return axios.get(`/api/admin/permissions/role/${roleName}`);
}

export function removeUserFromRole(user: IUserInRole, roleName: string) {
    return axios.delete(`/api/admin/permissions/role/${roleName}/user/${user.id}`);
}

export function addUserToRole(user: IUserInRole, roleName: string) {
    //user id is wrapped with doublequotes as single object in body so that api can deserialize a primitive in controller model bind
    return axios.put(`/api/admin/permissions/role/${roleName}/user`, `"${user.id}"`);
}