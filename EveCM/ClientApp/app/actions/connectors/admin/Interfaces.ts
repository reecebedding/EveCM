import { IUserInRole } from "../../../components/Admin/permissions/interfaces/Interfaces";

export interface IAdminPermissions {
    roles: string[]
}

export interface IRoleInformation {
    data: {
        name: string,
        users: IUserInRole[],
        usersToAdd: IUserInRole[]
    },
    ui: {
        userRemoved: boolean,
        userAdded: boolean
    }
}