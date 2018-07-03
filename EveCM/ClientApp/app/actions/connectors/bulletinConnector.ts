import axios from 'axios';
import { IBulletin } from '../../components/common/bulletins/interfaces/Interfaces';

export function getAllBulletins(){
    return axios.get('/api/bulletin');
}

export function saveNewBulletin(bulletin: IBulletin) {
    return axios.post('/api/bulletin', bulletin);
}