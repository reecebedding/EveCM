import { IBulletinStoreState, IAdminPermissionsStoreState } from '../store/IStoreState';
import { IUser } from '../components/common/userDetails/interfaces/Interfaces';

export const bulletinState: IBulletinStoreState = {
    bulletins: [],
    currentUser: {
        id: '0',
        primaryCharacterId: '0',
        avatarUrl: 'images/guest.png',
        userName: 'guest',
        roles: []
    }
}

export const adminPermissionsState: IAdminPermissionsStoreState = {
    adminPermissions: {
        roles: []
    },
    roleInformation: {
        name: '',
        users: []
    }
}