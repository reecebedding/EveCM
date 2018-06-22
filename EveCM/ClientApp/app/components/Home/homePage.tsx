import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';

import configureStore from '../../store/configureStore';
import BulletinBoard from '../common/bulletins/BulletinBoard';

import { loadBulletins } from '../../actions/bulletinActions';

const store = configureStore();
store.dispatch(loadBulletins());

ReactDOM.render(
    <Provider store={store}>
        <BulletinBoard />
    </Provider>,
    document.getElementById('bulletin-board')
);