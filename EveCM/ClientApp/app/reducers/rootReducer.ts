import { combineReducers } from 'redux';
import bulletins from './bulletinReducer';
import currentUser from './userReducer';
import { roleInformation, adminPermissions } from './adminPermissionsReducer';

const rootReducer = combineReducers({
    bulletins,
    currentUser,
    adminPermissions,
    roleInformation
});

export default rootReducer;