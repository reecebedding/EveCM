import { combineReducers } from 'redux';
import bulletins from './bulletinReducer';
import currentUser from './userReducer';

const rootReducer = combineReducers({
    bulletins,
    currentUser
});

export default rootReducer;