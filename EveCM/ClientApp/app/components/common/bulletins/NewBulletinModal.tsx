import * as React from 'react';
import { connect } from 'react-redux';
import { Button, Modal, ModalHeader, ModalBody, ModalFooter } from 'reactstrap';
import * as BulletinActions from '../../../actions/bulletinActions';
import { Dispatch } from 'redux';
import { IBulletinStoreState } from '../../../store/IStoreState';
import { IBulletin } from './interfaces/Interfaces';

interface IProps {
    active: boolean,
    toggle: () => void,
    save: (bulletin: IBulletin) => void
}

interface IState {
    bulletin: IBulletin
}

export class NewBulletinModal extends React.Component<IProps, IState> {

    getInitialBulletinState(): IBulletin {
        return {
            id: 0,
            title: '',
            content: '',
            date: new Date,
            authorCharacter: {
                userName: '',
                avatarUrl: ''
            }
        };
    }

    constructor(props: any) {
        super(props);

        this.state = {
            bulletin: this.getInitialBulletinState()
        };
    }

    saveBulletin = (e: any) => {
        this.props.save(this.state.bulletin);
        this.props.toggle();
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
            bulletin: this.getInitialBulletinState()
        }));
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
                        <textarea rows={10} className="form-control" value={this.state.bulletin.content} name="content" onChange={this.handleChange} />
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

function mapStateToProps(state: IBulletinStoreState) {
    return {
    };
}

function mapDispatchToProps(dispatch: Dispatch) {
    return {
        save: (bulletin: IBulletin) => dispatch(BulletinActions.saveBulletin(bulletin))
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(NewBulletinModal);