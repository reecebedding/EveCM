import { combineReducers } from 'redux';
import bulletins from './bulletinReducer';

const rootReducer = combineReducers({
    bulletins
});

export default rootReducer;