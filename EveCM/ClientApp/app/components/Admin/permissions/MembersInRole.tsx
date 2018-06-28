import * as React from 'react';
import { render } from 'react-dom';

import { Table, } from 'reactstrap';
import { IUserInRole } from './interfaces/Interfaces';

const MembersInRole: React.SFC<{ members: IUserInRole[] }> = (props) => (
    <Table responsive hover>
        <thead>
            <tr className="d-flex">
                <th className="col-1"/>
                <th className="col-3">Name</th>
                <th className="col-7">E-Mail</th>
                <th className="col-1" />
            </tr>
        </thead>
        <tbody>
            {
                props.members.map((member, index) => (
                    <tr className="d-flex" key={index}>
                        <td className="col-1"><img src={member.avatarUrl} width="48px" /></td>
                        <td className="col-3">{member.userName}</td>
                        <td className="col-7">{member.email}</td>
                        <td className="col-1" />
                    </tr>
                ))
            }
        </tbody>
    </Table>
);

export default MembersInRole;