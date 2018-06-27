import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { connect } from 'react-redux';
import { Dispatch } from 'redux';

import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

import * as Contracts from './interfaces/Interfaces';
import BulletinList from './BulletinList';
import * as BulletinActions from '../../../actions/bulletinActions';
import { IBulletin } from './interfaces/Interfaces';
import { IUser } from '../userDetails/interfaces/Interfaces';
import { IStoreState } from '../../../store/IStoreState';
import * as UserUtils from '../../../lib/user-utils';
import NewBulletinModal from './NewBulletinModal'

interface IProps {
    bulletins: IBulletin[],
    currentUser: IUser
}

interface IState {
    newBulletinVisible: boolean,
    newBulletin: IBulletin
}

export class BulletinBoard extends React.Component<IProps, IState> {

    constructor(props: any) {
        super(props);

        this.state = {
            newBulletinVisible: false
        }

        this.toggleNewBulletin = this.toggleNewBulletin.bind(this);
    }

    toggleNewBulletin() {
        this.setState(prev => {
            return Object.assign(prev, { newBulletinVisible: !prev.newBulletinVisible });
        });
    }

    render() {
        const { bulletins } = this.props;
        const { currentUser } = this.props;

        return (
            <div className="card">
                <div className="card-header">
                    <h4 className="float-left ">
                        News
                    </h4>
                    {
                        UserUtils.isInRole('admin', this.props.currentUser) && (
                            <div className="float-right">
                                <Button color="primary" size="sm" onClick={this.toggleNewBulletin}>
                                    <i className="fa fa-plus mr-2" />
                                    New Post
                                </Button>
                            </div>
                        )
                    }
                </div>
                <BulletinList bulletins={bulletins} />
                <NewBulletinModal active={this.state.newBulletinVisible} toggle={this.toggleNewBulletin} />
            </div>
        );
    }
}

function mapStateToProps(state: IStoreState) {
    return {
        bulletins: state.bulletins,
        currentUser: state.currentUser
    };
}

export default connect(mapStateToProps)(BulletinBoard);