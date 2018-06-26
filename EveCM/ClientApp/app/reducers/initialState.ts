import { IStoreState } from '../store/IStoreState';
import { IUser } from '../components/common/userDetails/interfaces/Interfaces';

const state: IStoreState = {
    bulletins: [],
    currentUser: {
        id: '0',
        primaryCharacterId: '0',
        avatarUrl: 'images/guest.png',
        userName: 'guest',
        roles: []
    }
}

export default state;