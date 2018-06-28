import * as React from 'react';

import { TabContent, TabPane, Nav, NavItem, NavLink, Card, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import { connect, Dispatch } from 'react-redux';
import { IAdminPermissionsStoreState } from '../../../store/IStoreState';
import { IAdminPermissions, IRoleInformation } from '../../../actions/connectors/admin/Interfaces';
import * as AdminActions from '../../../actions/adminActions';
import MembersInRole from './MembersInRole';

interface IProps {
    permissions: IAdminPermissions,
    loadPermissions: () => void,
    loadRoleInformation: (roleName: string) => void,
    roleInformation: IRoleInformation
}

interface IState {
    roleDropDownActive: boolean
}

class PermissionsDashboard extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            roleDropDownActive: false
        };

        this.toggleRoleDropDown = this.toggleRoleDropDown.bind(this);
        this.selectDropDown = this.selectDropDown.bind(this);

        this.props.loadPermissions();
    }

    toggleRoleDropDown() {
        this.setState((prevState) => ({
            ...prevState,
            roleDropDownActive: !prevState.roleDropDownActive
        }));
    }

    selectDropDown(roleName: string) {
        this.props.loadRoleInformation(roleName);
    }

    render() {
        return (
            <div>
                <div className="row no-gutters ml-2 mt-2 mb-2">
                    <div>
                        <Dropdown isOpen={this.state.roleDropDownActive} toggle={this.toggleRoleDropDown}>
                            <DropdownToggle caret>
                                Select Role
                            </DropdownToggle>
                            <DropdownMenu>
                                {
                                    this.props.permissions.roles.map((role, index) => (
                                        <DropdownItem key={index} onClick={() => this.selectDropDown(role)}>{role}</DropdownItem>
                                    ))
                                }
                            </DropdownMenu>
                        </Dropdown>
                    </div>
                </div>
                <div className="ml-2 pt-2">
                    {
                        this.props.roleInformation.name && (
                            <h3>
                                Role: {this.props.roleInformation.name}
                            </h3>
                        )
                    }
                </div>
                <div>
                    {
                        this.props.roleInformation.users.length > 0 ?
                            <MembersInRole members={this.props.roleInformation.users} />
                            : this.props.roleInformation.name && (
                                <div>
                                    <h3>
                                        No members in role: {this.props.roleInformation.name}
                                    </h3>
                                </div>
                            )
                    }
                </div>
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
        loadRoleInformation: (roleName: string) => dispatch(AdminActions.loadRoleInformation(roleName))
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(PermissionsDashboard);