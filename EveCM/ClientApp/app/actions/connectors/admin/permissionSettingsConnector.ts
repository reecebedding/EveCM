import axios from 'axios';
import { IUserInRole } from '../../../components/Admin/permissions/interfaces/Interfaces';

export function getPermissionSettings() {
    return axios.get('/api/admin/permissions');
}

export function getRoleInformation(roleName: string) {
    return axios.get(`/api/admin/permissions/role/${roleName}`);
}

export function removeUserFromRole(user: IUserInRole, roleName: string) {
    return axios.delete(`/api/admin/permissions/role/${roleName}/user/${user.id}`);
}