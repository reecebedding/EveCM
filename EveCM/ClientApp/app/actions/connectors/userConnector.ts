import axios from 'axios';
import { IUser } from '../../components/common/userDetails/interfaces/Interfaces';

export function getCurrentUser() {
    return axios.get('/api/user/me');
}