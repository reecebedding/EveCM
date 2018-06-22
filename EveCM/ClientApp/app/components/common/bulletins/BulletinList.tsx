import * as Contracts from './interfaces/Interfaces';
import * as React from 'react';

export default class BulletinList extends React.Component<{ bulletins: Contracts.IBulletin[] }, any> {
    render() {
        debugger;
        return this.props.bulletins.map((bulletin, index) => (
            <div key={bulletin.id}>
                <div className="card-body">
                    <h5 className="card-title">{bulletin.title}</h5>
                    <p className="card-text">{bulletin.content}</p>
                    <footer className="blockquote-footer">
                        <img src={bulletin.authorCharacter.avatarUrl} />
                        {bulletin.authorCharacter.userName} | {new Date(bulletin.date).toDateString()}
                    </footer>
                </div>
                {this.props.bulletins.length - 1 !== index && <hr />}
            </div>
        ));
    }
}