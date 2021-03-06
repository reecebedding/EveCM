﻿import * as React from 'react';

import { TabContent, TabPane, Nav, NavItem, NavLink, Card, Dropdown, DropdownToggle, DropdownMenu, DropdownItem, Button } from 'reactstrap';

import { connect } from 'react-redux';
import { Dispatch, AnyAction } from 'redux';
import { IAdminPermissionsStoreState } from '../../../store/IStoreState';
import { IAdminPermissions, IRoleInformation } from '../../../actions/connectors/admin/Interfaces';
import * as AdminActions from '../../../actions/adminActions';
import MembersInRole from './MembersInRole';
import { IUserInRole } from './interfaces/Interfaces';
import ConfirmModal from '../../common/dialogs/confirmModal';
import AddMemberToRoleInput from './AddMemberToRoleInput';
import { ThunkDispatch } from 'redux-thunk';
import { NotificationAlertContainer, NotificationAlertManager, NotificationType } from '../../common/notifications/NotificationAlert';

interface IProps {
    permissions: IAdminPermissions,
    dismissRemoveUserFromRole: () => any,
    loadPermissions: () => any,
    loadRoleInformation: (roleName: string) => any,
    roleInformation: IRoleInformation,
    removeUserFromRole: (user: IUserInRole, roleName: string) => any
}

interface IState {
    roleDropDownActive: boolean,
    displayConfirmRemove: boolean,
    userToRemove: IUserInRole
}

class PermissionsDashboard extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            roleDropDownActive: false,
            displayConfirmRemove: false,
            userToRemove: this.constructDefaultUser()
        };
    }

    componentDidMount() {
        this.props.loadPermissions();
    }

    componentDidUpdate() {
        if (this.props.roleInformation.ui.userRemoved) {
            this.props.dismissRemoveUserFromRole();
            NotificationAlertManager.alert(NotificationType.Success, 'User successfully removed');
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

    selectRoleDropDown = (roleName: string) => {
        this.props.loadRoleInformation(roleName);
    }

    toggleRoleSelectDropDown = () => {
        this.setState((prevState) => ({
            ...prevState,
            roleDropDownActive: !prevState.roleDropDownActive
        }));
    }

    toggleConfirmRemoveModal = (user: IUserInRole) => {
        this.setState((prevState) => ({
            ...prevState,
            displayConfirmRemove: !prevState.displayConfirmRemove,
            userToRemove: user
        }));
    }

    removeMemberFromRole = () => {
        this.props.removeUserFromRole(this.state.userToRemove, this.props.roleInformation.data.name);
    }

    render() {
        return (
            <div>
                <div className="row no-gutters ml-2 mt-2 mb-2">
                    <div>
                        <Dropdown isOpen={this.state.roleDropDownActive} toggle={this.toggleRoleSelectDropDown}>
                            <DropdownToggle caret={true}>
                                Select Role
                            </DropdownToggle>
                            <DropdownMenu>
                                {
                                    this.props.permissions.roles.map((role, index) => (
                                        <DropdownItem key={index} onClick={this.selectRoleDropDown.bind(this, role)}>{role}</DropdownItem>
                                    ))
                                }
                            </DropdownMenu>
                        </Dropdown>
                    </div>
                </div>
                <div>
                    <div>
                        {
                            this.props.roleInformation.data.name && (
                                <h3 className="lead text-center">{this.props.roleInformation.data.name}</h3>
                            )
                        }
                    </div>
                </div>
                {
                    this.props.roleInformation.data.name !== '' && (
                        <AddMemberToRoleInput
                            roleInformation={this.props.roleInformation}
                        />
                    )
                }

                <div>
                    {
                        this.props.roleInformation.data.users.length > 0 ?
                            <MembersInRole members={this.props.roleInformation.data.users} deleteMember={this.toggleConfirmRemoveModal} />
                            : this.props.roleInformation.data.name && (
                                <div className="ml-2">
                                    <p>No members</p>
                                </div>
                            )
                    }
                </div>
                <NotificationAlertContainer />
                <ConfirmModal
                    active={this.state.displayConfirmRemove}
                    toggle={this.toggleConfirmRemoveModal.bind(this, this.constructDefaultUser())}
                    onConfirm={this.removeMemberFromRole}
                    title='Confirm remove user'
                    body={`Are you sure you want to remove ${this.state.userToRemove.userName} from ${this.props.roleInformation.data.name}?`}
                />
            </div>
        );
    }
}


function mapStateToProps(state: IAdminPermissionsStoreState) {
    return {
        permissions: state.adminPermissions,
        roleInformation: state.roleInformation
    };
}

function mapDispatchToProps(dispatch: ThunkDispatch<IAdminPermissionsStoreState, null, AnyAction>) {
    return {
        loadPermissions: () => dispatch(AdminActions.loadAdminPermissions()),
        loadRoleInformation: (roleName: string) => dispatch(AdminActions.loadRoleInformation(roleName)),
        removeUserFromRole: (user: IUserInRole, roleName: string) => dispatch(AdminActions.removeMemberFromRole(user, roleName)),
        dismissRemoveUserFromRole: () => dispatch(AdminActions.dismissRemoveMemberFromRoleSuccess())
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(PermissionsDashboard);