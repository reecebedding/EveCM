import * as React from 'react';
import { Modal, ModalHeader, ModalBody, ModalFooter, Button } from 'reactstrap';

interface IProps {
    active: boolean,
    toggle: () => void,
    onConfirm: () => void,
    onDecline?: () => void,
    title: string,
    body: string,
    confirmButtonText?: string,
    declineButtonText?: string
}

export default class ConfirmModal extends React.Component<IProps> {
    static defaultProps = {
        confirmButtonText: 'Yes',
        declineButtonText: 'No',
        onDecline: () => { }
    }

    decline = () => {
        if (this.props.onDecline) {
            this.props.onDecline();
        }
        this.props.toggle();
    }

    confirm = () => {
        this.props.onConfirm();
        this.props.toggle();
    }

    render() {
        return (
            <Modal isOpen={this.props.active} toggle={this.props.toggle} backdrop="static">
                <ModalHeader toggle={this.props.toggle}>{this.props.title}</ModalHeader>
                <ModalBody>
                    {this.props.body}
                </ModalBody>
                <ModalFooter>
                    <Button color="primary" onClick={this.confirm}>{this.props.confirmButtonText}</Button>
                    <Button color="secondary" onClick={this.decline}>{this.props.declineButtonText}</Button>
                </ModalFooter>
            </Modal>
        );
    }
}