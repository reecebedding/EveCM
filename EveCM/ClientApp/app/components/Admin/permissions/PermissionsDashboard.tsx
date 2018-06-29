import * as React from 'react';

import { TabContent, TabPane, Nav, NavItem, NavLink, Card, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import { NotificationContainer, NotificationManager } from 'react-notifications';

import { connect, Dispatch } from 'react-redux';
import { IAdminPermissionsStoreState } from '../../../store/IStoreState';
import { IAdminPermissions, IRoleInformation } from '../../../actions/connectors/admin/Interfaces';
import * as AdminActions from '../../../actions/adminActions';
import MembersInRole from './MembersInRole';
import { IUserInRole } from './interfaces/Interfaces';
import ConfirmModal from '../../common/dialogs/confirmModal';

interface IProps {
    permissions: IAdminPermissions,
    dismissRemoveUserFromRole: () => void,
    loadPermissions: () => void,
    loadRoleInformation: (roleName: string) => void,
    roleInformation: IRoleInformation,
    removeUserFromRole: (user: IUserInRole, roleName: string) => void
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

    constructDefaultUser(): IUserInRole {
        return {
            avatarUrl: '',
            email: '',
            id: '',
            userName: ''
        };
    }

    toggleRoleDropDown = () => {
        this.setState((prevState) => ({
            ...prevState,
            roleDropDownActive: !prevState.roleDropDownActive
        }));
    }

    toggleConfirmRemove = (user: IUserInRole) => {
        this.setState((prevState) => ({
            ...prevState,
            displayConfirmRemove: !prevState.displayConfirmRemove,
            userToRemove: user
        }));
    }

    selectDropDown = (roleName: string) => {
        this.props.loadRoleInformation(roleName);
    }

    removeMemberFromRole = () => {
        this.props.removeUserFromRole(this.state.userToRemove, this.props.roleInformation.data.name);
    }

    componentDidUpdate() {
        if (this.props.roleInformation.ui.userRemoved) {
            this.props.dismissRemoveUserFromRole();
            NotificationManager.success('User successfully removed', '', 0);
        }
    }

    render() {
        return (
            <div>
                <div className="row no-gutters ml-2 mt-2 mb-2">
                    <div>
                        <Dropdown isOpen={this.state.roleDropDownActive} toggle={this.toggleRoleDropDown}>
                            <DropdownToggle caret={true}>
                                Select Role
                            </DropdownToggle>
                            <DropdownMenu>
                                {
                                    this.props.permissions.roles.map((role, index) => (
                                        <DropdownItem key={index} onClick={this.selectDropDown.bind(this, role)}>{role}</DropdownItem>
                                    ))
                                }
                            </DropdownMenu>
                        </Dropdown>
                    </div>
                </div>
                <div className="ml-2 pt-2">
                    {
                        this.props.roleInformation.data.name && (
                            <h3>
                                Role: {this.props.roleInformation.data.name}
                            </h3>
                        )
                    }
                </div>
                <div>
                    {
                        this.props.roleInformation.data.users.length > 0 ?
                            <MembersInRole members={this.props.roleInformation.data.users} deleteMember={this.toggleConfirmRemove} />
                            : this.props.roleInformation.data.name && (
                                <div className="ml-2">
                                    <p>No members</p>
                                </div>
                            )
                    }
                </div>
                <NotificationContainer />
                <ConfirmModal
                    active={this.state.displayConfirmRemove}
                    toggle={this.toggleConfirmRemove.bind(this, this.constructDefaultUser())}
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

function mapDispatchToProps(dispatch: Dispatch) {
    return {
        loadPermissions: () => dispatch(AdminActions.loadAdminPermissions()),
        loadRoleInformation: (roleName: string) => dispatch(AdminActions.loadRoleInformation(roleName)),
        removeUserFromRole: (user: IUserInRole, roleName: string) => dispatch(AdminActions.removeMemberFromRole(user, roleName)),
        dismissRemoveUserFromRole: () => dispatch(AdminActions.dismissRemoveMemberFromRoleSuccess())
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(PermissionsDashboard);