import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import 'react-notifications/lib/notifications.css';

import AdminDashboard from './AdminDashBoard';

import configureStore from '../../store/configureStore';
import { bulletinState } from '../../reducers/initialState';
import { loadCurrentUser } from '../../actions/userActions';

const store = configureStore(bulletinState);
store.dispatch(loadCurrentUser());

ReactDOM.render(
    <Provider store={store}>
        <AdminDashboard />
    </Provider>,
    document.getElementById('admin-control-panel')
);