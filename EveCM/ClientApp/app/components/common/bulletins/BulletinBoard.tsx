﻿import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { connect } from 'react-redux';
import { Dispatch, AnyAction } from 'redux';

import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';

import * as Contracts from './interfaces/Interfaces';
import BulletinList from './BulletinList';
import * as BulletinActions from '../../../actions/bulletinActions';
import { IBulletin } from './interfaces/Interfaces';
import { IUser } from '../userDetails/interfaces/Interfaces';
import { IBulletinStoreState } from '../../../store/IStoreState';
import * as UserUtils from '../../../lib/user-utils';
import NewBulletinModal from './NewBulletinModal'
import { ThunkDispatch } from 'redux-thunk';
import { removeBulletin } from '../../../actions/bulletinActions';
import ConfirmModal from '../dialogs/confirmModal';

interface IProps {
    bulletins: IBulletin[],
    currentUser: IUser,
    removeBulletinAction: (bulletin: IBulletin) => any
}

interface IState {
    newBulletinVisible: boolean,
    removeBulletinVisible: boolean,
    bulletinToRemove: IBulletin
}

export class BulletinBoard extends React.Component<IProps, IState> {

    constructor(props: any) {
        super(props);

        this.state = {
            newBulletinVisible: false,
            removeBulletinVisible: false,
            bulletinToRemove: this.constructDefaultBulletin()
        }
    }
    constructDefaultBulletin(): IBulletin {
        return {
            authorCharacter: {
                avatarUrl: '',
                userName: ''
            },
            content: '',
            date: new Date(),
            id: 0,
            title: ''
        };
    }

    toggleNewBulletin = () => {
        this.setState(prev => {
            return Object.assign(prev, { newBulletinVisible: !prev.newBulletinVisible });
        });
    }

    toggleRemoveBulletin = (bulletin: IBulletin) => {
        this.setState(prev => {
            return Object.assign(prev, {
                removeBulletinVisible: !prev.removeBulletinVisible,
                bulletinToRemove: (prev.removeBulletinVisible)? this.constructDefaultBulletin() : bulletin
            });
        })
    }

    removeBulletin = () => {
        if (this.state.bulletinToRemove != this.constructDefaultBulletin()) {
            this.props.removeBulletinAction(this.state.bulletinToRemove);
        }
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
                                    <i className="fas fa-plus mr-2" />
                                    New Post
                                </Button>
                            </div>
                        )
                    }
                </div>

                <BulletinList bulletins={bulletins} removeBulletinAction={this.toggleRemoveBulletin} />

                <NewBulletinModal active={this.state.newBulletinVisible} toggle={this.toggleNewBulletin} />

                <ConfirmModal
                    active={this.state.removeBulletinVisible}
                    toggle={this.toggleRemoveBulletin.bind(this, this.constructDefaultBulletin())}
                    onConfirm={this.removeBulletin}
                    title='Confirm bulletin deletion'
                    body={`Are you sure you want to remove the bulletin: "${this.state.bulletinToRemove.title}" ?`}
                />
            </div>
        );
    }
}

function mapStateToProps(state: IBulletinStoreState) {
    return {
        bulletins: state.bulletins,
        currentUser: state.currentUser
    };
}

function mapDispatchToProps(dispatch: ThunkDispatch<IBulletinStoreState, null, AnyAction>) {
    return {
        removeBulletinAction: (bulletin: IBulletin) => dispatch(BulletinActions.removeBulletin(bulletin))
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(BulletinBoard);