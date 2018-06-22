import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { connect } from 'react-redux';
import { Dispatch } from 'redux';
import * as Contracts from './interfaces/Interfaces';
import BulletinList from './BulletinList';
import { IBulletin } from './interfaces/Interfaces';
import { IStoreState } from '../../../store/IStoreState';

interface IProps {
    bulletins: IBulletin[]
}

export class BulletinBoard extends React.Component<IProps> {

    constructor(props: any) {
        super(props);
    }

    render() {
       const { bulletins } = this.props;

        return (
            <div className="card">
                <div className="card-header">
                    Notifications
                    <span className="float-right text-gray">Showing {bulletins.length} latest</span>
                </div>
                <BulletinList bulletins={bulletins} />
            </div>
        );
    }
}

function mapStateToProps(state: IStoreState) {
    return {
        bulletins: state.bulletins
    };
}

function mapDispatchToProps(dispatch: Dispatch) {
    return {

    };
}

export default connect(mapStateToProps, mapDispatchToProps)(BulletinBoard);