import * as React from 'react';
import { render } from 'react-dom';

import { Table, } from 'reactstrap';
import { IUserInRole } from './interfaces/Interfaces';

export interface IProps {
    members: IUserInRole[],
    deleteMember: (user: IUserInRole) => void
}

const MembersInRole: React.SFC<IProps> = (props) => (
    <Table responsive hover>
        <thead>
            <tr className="d-flex">
                <th className="col-1"/>
                <th className="col-3">Name</th>
                <th className="col-5">E-Mail</th>
                <th className="col-3" />
            </tr>
        </thead>
        <tbody>
            {
                props.members.map((member, index) => (
                    <tr className="d-flex" key={index}>
                        <td className="col-1"><img src={member.avatarUrl} width="48px" /></td>
                        <td className="col-3">{member.userName}</td>
                        <td className="col-5">{member.email}</td>
                        <td className="col-3">
                            <button type="button" className="btn btn-danger" onClick={() => props.deleteMember(member)}>
                                <i className="fas fa-user-minus"></i>
                            </button>
                        </td>
                    </tr>
                ))
            }
        </tbody>
    </Table>
);

export default MembersInRole;