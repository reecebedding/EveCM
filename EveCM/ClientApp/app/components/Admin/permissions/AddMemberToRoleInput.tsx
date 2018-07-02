import * as React from 'react';
import ConfirmModal from '../../common/dialogs/confirmModal';
import { Button } from 'reactstrap';
import Select from 'react-select'
import { IUserInRole } from './interfaces/Interfaces';
import { IRoleInformation } from '../../../actions/connectors/admin/Interfaces';
import { NotificationContainer, NotificationManager } from 'react-notifications';
import { IAdminPermissionsStoreState } from '../../../store/IStoreState';
import { Dispatch } from 'redux';
import * as AdminActions from '../../../actions/adminActions';
import { connect } from 'react-redux';

interface IProps {
    addUserAction: (user: IUserInRole, roleName: string) => any,
    dismissAddUserToRole: () => any,
    roleInformation: IRoleInformation
}

interface IState {
    displayConfirmAddToRole: boolean,
    selectedUserToAdd: IUserInRole
}

class AddMemberToRoleInput extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);
        this.state = {
            displayConfirmAddToRole: false,
            selectedUserToAdd: this.constructDefaultUser()
        }
    }

    componentDidUpdate() {
        if (this.props.roleInformation.ui.userAdded) {
            this.props.dismissAddUserToRole();
            NotificationManager.success('User successfully added', '', 0);
        }
    }

    constructDefaultUser(): IUserInRole {
        return {
            avatarUrl: '',
            email: '',
            id: '',
            userName: ''
        };
    }

    renderUserToAddOption(user: IUserInRole) {
        return (
            <div>
                <img src={user.avatarUrl} width="24px" /> {user.userName}
            </div>
        );
    }

    toggleAddMemberConfirmModal = () => {
        if (this.state.displayConfirmAddToRole || this.canAddMemberToRole()) {
            this.setState((prev) => ({
                displayConfirmAddToRole: !prev.displayConfirmAddToRole
            }));
        }
    }

    addMemberToRole = () => {
        this.props.addUserAction(this.state.selectedUserToAdd, this.props.roleInformation.data.name);
        this.setState({
            selectedUserToAdd: this.constructDefaultUser()
        });
    }

    canAddMemberToRole = () => {
        return !this.state.displayConfirmAddToRole && this.state.selectedUserToAdd.userName && this.state.selectedUserToAdd.userName !== this.constructDefaultUser().userName;
    }

    handleChangeInUserToAddDropDown = (selectedOption: IUserInRole) => {
        const valueToAdd = (selectedOption !== null) ? selectedOption : this.constructDefaultUser();
        this.setState({ selectedUserToAdd: valueToAdd });
    }

    render() {
        return (
            <div>
                <div className="col-4 pb-2 no-gutters">
                    <div className="row no-gutters">
                        <div className="col-5 pr-1">
                            <Select
                                value={this.state.selectedUserToAdd}
                                onChange={this.handleChangeInUserToAddDropDown}
                                options={this.props.roleInformation.data.usersToAdd}
                                labelKey="userName"
                                openOnClick={false}
                                openOnFocus={false}
                                optionRenderer={this.renderUserToAddOption}
                            />
                        </div>
                        <div className="col-3 pt-1">
                            <Button color="primary" size="sm" onClick={this.toggleAddMemberConfirmModal} active={!this.canAddMemberToRole()}><i className="fas fa-plus" /> Add to role</Button>
                        </div>
                    </div>
                </div>
                <ConfirmModal
                    active={this.state.displayConfirmAddToRole}
                    toggle={this.toggleAddMemberConfirmModal}
                    onConfirm={this.addMemberToRole}
                    title='Confirm add user'
                    body={`Are you sure you want to add ${this.state.selectedUserToAdd.userName} to ${this.props.roleInformation.data.name}?`}
                />
                <NotificationContainer />
            </div>
        );
    }
}

function mapStateToProps(state: IAdminPermissionsStoreState) {
    return {
    };
}

function mapDispatchToProps(dispatch: Dispatch) {
    return {
        dismissAddUserToRole: () => dispatch(AdminActions.dismissAddMemberToRoleSuccess()),
        addUserAction: (user: IUserInRole, roleName: string) => dispatch(AdminActions.addMemberToRole(user, roleName))
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(AddMemberToRoleInput);