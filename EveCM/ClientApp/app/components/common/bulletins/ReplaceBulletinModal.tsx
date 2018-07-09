import * as React from 'react';
import * as BulletinActions from '../../../actions/bulletinActions';
import { IBulletin } from './interfaces/Interfaces';
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from 'reactstrap';
import { IBulletinStoreState } from '../../../store/IStoreState';
import { ThunkDispatch } from 'redux-thunk';
import { AnyAction } from 'redux';
import { connect } from 'react-redux';

interface IProps {
    active: boolean,
    toggle: (bulletin: IBulletin) => void,
    bulletin: IBulletin,
    replaceAction: (bulletin: IBulletin) => any
}


interface IState {
    bulletin: IBulletin
}

class ReplaceBulletinModal extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);
        
        this.state = {
            bulletin: this.props.bulletin
        };
    }

    constructDefaultBulletin(): IBulletin {
        return {
            authorCharacter: {
                avatarUrl: '',
                userName: ''
            },
            content: '',
            createdDate: new Date(),
            id: 0,
            title: ''
        };
    }

    replaceBulletin = (e: any) => {
        this.props.replaceAction(this.state.bulletin);
        this.props.toggle(this.state.bulletin);
    }

    handleChange = (event: any) => {
        let property = event.target.name;
        let value = event.target.value;

        this.setState((prevState) => ({
            ...prevState,
            bulletin: {
                ...prevState.bulletin,
                [property]: value
            }
        }));
    }

    formClosed = () => {
        this.setState((prevState, props) => ({
            bulletin: this.constructDefaultBulletin()
        }));
    }

    isSubmittable(): boolean {
        return this.state.bulletin.title.length > 0
            && this.state.bulletin.content.length > 0;
    }

    handleToggle = () => {
        this.props.toggle(this.state.bulletin);
    }

    render() {
        return (
            <Modal isOpen={this.props.active} toggle={this.handleToggle} onClosed={this.formClosed} className="modal-lg">
                <ModalHeader toggle={this.handleToggle}>Update Post</ModalHeader>
                <ModalBody>
                    <div className="form-group">
                        <label>Title</label>
                        <input type="text" className="form-control" value={this.state.bulletin.title} name="title" onChange={this.handleChange} />
                    </div>
                    <div className="form-group">
                        <label>Message</label>
                        <textarea rows={10} className="form-control" value={this.state.bulletin.content} name="content" onChange={this.handleChange} />
                    </div>
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={this.replaceBulletin} disabled={!this.isSubmittable()}>Save</Button>
                    <Button color="secondary" onClick={this.handleToggle}>Cancel</Button>
                </ModalFooter>
            </Modal>
        );
    }
}

function mapStateToProps(state: IBulletinStoreState) {
    return {
    };
}

function mapDispatchToProps(dispatch: ThunkDispatch<IBulletinStoreState, null, AnyAction>) {
    return {
        replaceAction: (bulletin: IBulletin) => dispatch(BulletinActions.replaceBulletin(bulletin))
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(ReplaceBulletinModal);