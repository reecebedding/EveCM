import * as React from 'react';
import { IUser } from '../common/userDetails/interfaces/Interfaces';
import { IBulletinStoreState } from '../../store/IStoreState';
import { Dispatch } from 'redux';
import { connect } from 'react-redux';
import classnames from 'classnames';

import PermissionDashboard from './permissions/PermissionsDashboard';
import { TabContent, TabPane, Nav, NavItem, NavLink, Card } from 'reactstrap';

interface IProps {
    currentUser: IUser
}

interface IState {
    activeTabName: string
}

export class AdminDashboard extends React.Component<IProps, IState> {

    constructor(props: IProps) {
        super(props);

        this.state = {
            activeTabName: 'permissions'
        };

        this.toggleTab = this.toggleTab.bind(this);
    }

    toggleTab(tabName: string) {
        if (this.state.activeTabName !== tabName) {
            this.setState((prevState) => ({
                ...prevState,
                activeTabName: tabName
            }));
        }
    }
    
    render() {
        return (
            <div>
                <Nav tabs>
                    <NavItem>
                        <NavLink className={classnames({ active: this.state.activeTabName === 'permissions' })} onClick={() => { this.toggleTab('permissions'); }} >
                            Permissions
                        </NavLink>
                    </NavItem>
                </Nav>
                <TabContent activeTab={this.state.activeTabName}>
                    <TabPane tabId="permissions">
                        <Card>
                            <PermissionDashboard />
                        </Card>
                    </TabPane>
                </TabContent>
            </div>
        );
    }
}

function mapStateToProps(state: IBulletinStoreState) {
    return {
        currentUser: state.currentUser
    };
}

export default connect(mapStateToProps)(AdminDashboard);