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

const ConfirmModal: React.SFC<IProps> = (props) => (
    <Modal isOpen={props.active} toggle={props.toggle} backdrop="static">
        <ModalHeader toggle={props.toggle}>{props.title}</ModalHeader>
        <ModalBody>
            {props.body}
        </ModalBody>
        <ModalFooter>
            <Button color="primary" onClick={() => { props.onConfirm(); props.toggle(); }}>{props.confirmButtonText}</Button>
            <Button color="secondary" onClick={() => { props.onDecline(); props.toggle(); }}>{props.declineButtonText}</Button>
        </ModalFooter>
    </Modal>
);

ConfirmModal.defaultProps = {
    confirmButtonText: 'Yes',
    declineButtonText: 'No',
    onDecline: () => { }
}

export default ConfirmModal;