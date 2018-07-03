import { IBulletin } from '../components/common/bulletins/interfaces/Interfaces';
import { IUser } from '../components/common/userDetails/interfaces/Interfaces';
import { IAdminPermissions, IRoleInformation } from '../actions/connectors/admin/Interfaces';

export interface IBulletinStoreState {
    bulletins: IBulletin[],
    currentUser: IUser
};

export interface IAdminPermissionsStoreState {
    adminPermissions: IAdminPermissions,
    roleInformation: IRoleInformation
}