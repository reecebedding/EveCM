import * as Contracts from './interfaces/Interfaces';
import * as React from 'react';
import { IBulletin } from './interfaces/Interfaces';
import Bulletin from './Bulletin';

interface IProps {
    bulletins: Contracts.IBulletin[],
    removeBulletinAction: (bulletin: IBulletin) => any
}

export default class BulletinList extends React.Component<IProps> {
    
    render() {
        if (this.props.bulletins.length > 0) {
            return this.props.bulletins.map((bulletin, index) => (
                <div key={bulletin.id}>
                    <Bulletin bulletin={bulletin} removeBulletinAction={this.props.removeBulletinAction}/>
                    {this.props.bulletins.length - 1 !== index && <hr />}
                </div>
            ));
        }
        else {
            return (
                <div>
                    <div className="card-body">
                        <h5 className="card-title">No News</h5>
                    </div>
                </div>
            );
        }
    }
}
