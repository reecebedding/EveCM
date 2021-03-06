﻿import axios from 'axios';
import { IBulletin } from '../../components/common/bulletins/interfaces/Interfaces';

export function getAllBulletins(){
    return axios.get('/api/bulletin');
}

export function saveNewBulletin(bulletin: IBulletin) {
    return axios.post('/api/bulletin', bulletin);
}

export function removeBulletin(bulletin: IBulletin) {
    return axios.delete(`/api/bulletin/${bulletin.id}`);
}

export function replaceBulletin(bulletin: IBulletin) {
    return axios.put(`/api/bulletin/${bulletin.id}`, bulletin);
}