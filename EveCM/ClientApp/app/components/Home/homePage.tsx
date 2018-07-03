import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import configureStore from '../../store/configureStore';
import { bulletinState } from '../../reducers/initialState';

import BulletinBoard from '../common/bulletins/BulletinBoard';

import { loadBulletins } from '../../actions/bulletinActions';
import { loadCurrentUser } from '../../actions/userActions';

const store = configureStore(bulletinState);
store.dispatch(loadBulletins());
store.dispatch(loadCurrentUser());

ReactDOM.render(
    <Provider store={store}>
        <BulletinBoard />
    </Provider>,
    document.getElementById('bulletin-board')
);