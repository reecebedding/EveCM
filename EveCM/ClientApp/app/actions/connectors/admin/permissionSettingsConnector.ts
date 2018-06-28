import axios from 'axios';

export function getPermissionSettings() {
    return axios.get('/api/admin/permissions');
}

export function getRoleInformation(roleName: string) {
    return axios.get(`/api/admin/permissions/role/${roleName}`);
}