import axios from 'axios';
import { IBulletin } from '../../components/common/bulletins/interfaces/Interfaces';

export function getAllBulletins(){
    return axios.get('/bulletin');
}