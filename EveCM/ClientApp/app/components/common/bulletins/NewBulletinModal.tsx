import * as React from 'react';
import { connect } from 'react-redux';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import * as BulletinActions from '../../../actions/bulletinActions';
import { Dispatch } from 'redux';
import { IStoreState } from '../../../store/IStoreState';
import { IBulletin } from './interfaces/Interfaces';

interface IProps {
    active: boolean,
    toggle: () => void,
    save: (bulletin: IBulletin) => void,
    bulletinState: IBulletin
}

interface IState {
    bulletin: IBulletin
}

export class NewBulletinModal extends React.Component<IProps, IState> {

    constructor(props: any) {
        super(props);

        this.state = {
            bulletin: this.props.bulletinState
        };

        this.saveBulletin = this.saveBulletin.bind(this);
        this.handleChange = this.handleChange.bind(this);
        this.formClosed = this.formClosed.bind(this);
    }

    saveBulletin(e: any) {
        this.props.save(this.state.bulletin);
        this.props.toggle();
    }

    handleChange(event: any) {
        const nextState = {
            ...this.state,
            bulletin: {
                ...this.state.bulletin,
                [event.target.name]: event.target.value
            }
        };
        this.setState(nextState);
    }

    formClosed() {
        this.setState({ bulletin: this.props.bulletinState });
    }

    isSubmittable(): boolean {
        return this.state.bulletin.title.length > 0
            && this.state.bulletin.content.length > 0;
    }

    render() {
        return (
            <Modal isOpen={this.props.active} toggle={this.props.toggle} onClosed={this.formClosed} className="modal-lg">
                <ModalHeader toggle={this.props.toggle}>New Post</ModalHeader>
                <ModalBody>
                    <div className="form-group">
                        <label>Title</label>
                        <input type="text" className="form-control" value={this.state.bulletin.title} name="title" onChange={this.handleChange} />
                    </div>
                    <div className="form-group">
                        <label>Message</label>
                        <textarea rows={10} className="form-control" value={this.state.bulletin.content} name="content" onChange={this.handleChange}/>
                    </div>
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={this.saveBulletin} disabled={!this.isSubmittable()}>Save</Button>
                    <Button color="secondary" onClick={this.props.toggle}>Cancel</Button>
                </ModalFooter>
            </Modal>
        );
    }
}

function mapStateToProps(state: IStoreState) {
    return {
    };
}

function mapDispatchToProps(dispatch: Dispatch) {
    return {
        save: (bulletin: IBulletin) => dispatch(BulletinActions.saveBulletin(bulletin))
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(NewBulletinModal);