import { IBulletin } from '../components/common/bulletins/interfaces/Interfaces';
import { IUser } from '../components/common/userDetails/interfaces/Interfaces';

export interface IStoreState {
    bulletins: IBulletin[],
    currentUser: IUser
};