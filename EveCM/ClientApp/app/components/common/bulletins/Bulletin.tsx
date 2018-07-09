import * as React from 'react';
import { IBulletin } from './interfaces/Interfaces';

interface IProps {
    bulletin: IBulletin,
    removeBulletinAction: (bulletin: IBulletin) => any
}

export default class Bulletin extends React.Component<IProps> {

    constructor(props: IProps) {
        super(props);
    }

    handleRemove = () => {
        this.props.removeBulletinAction(this.props.bulletin);
    }

    render() {
        return (
            <div className="card-body">
                <div className="card-title">
                    <div className="">
                        <h5>{this.props.bulletin.title}</h5>
                    </div>
                    <div className="float-right">
                        <button type="button" className="btn btn-danger" onClick={this.handleRemove}>
                            <i className="fas fa-trash" />
                        </button>
                    </div>
                </div>
                <p className="card-text">{this.props.bulletin.content}</p>
                <footer className="blockquote-footer">
                    <img src={this.props.bulletin.authorCharacter.avatarUrl} className="mr-2" width="32px" />
                    {this.props.bulletin.authorCharacter.userName} - {new Date(this.props.bulletin.date).toLocaleDateString()}
                </footer>
            </div>
        );
    }
}