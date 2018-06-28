import { IUserInRole } from "../../../components/Admin/permissions/interfaces/Interfaces";

export interface IAdminPermissions {
    roles: string[]
}

export interface IRoleInformation {
    name: string,
    users: IUserInRole[]
}