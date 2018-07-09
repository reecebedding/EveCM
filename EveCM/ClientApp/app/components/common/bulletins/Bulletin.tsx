import * as React from 'react';
import { IBulletin } from './interfaces/Interfaces';

interface IProps {
    bulletin: IBulletin,
    removeBulletinAction: (bulletin: IBulletin) => any,
    replaceBulletinAction: (bulletin: IBulletin) => any
}

export default class Bulletin extends React.Component<IProps> {

    constructor(props: IProps) {
        super(props);
    }

    handleRemove = () => {
        this.props.removeBulletinAction(this.props.bulletin);
    }

    handleReplace = () => {
        this.props.replaceBulletinAction(this.props.bulletin);
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
                            <i className="fas fa-trash fa-fw" />
                        </button>
                        <button type="button" className="btn btn-warning ml-1" onClick={this.handleReplace}>
                            <i className="fas fa-pencil-alt fa-fw" />
                        </button>
                    </div>
                </div>
                <p className="card-text">{this.props.bulletin.content}</p>
                <footer className="blockquote-footer">
                    <img src={this.props.bulletin.authorCharacter.avatarUrl} className="mr-2" width="32px" />
                    {this.props.bulletin.authorCharacter.userName} -
                    <span className="pl-1">{new Date(this.props.bulletin.createdDate).toLocaleDateString()}</span>
                    {this.props.bulletin.updatedDate && <span className="pl-2">(updated: {new Date(this.props.bulletin.updatedDate).toLocaleDateString()})</span>}
                </footer>
            </div>
        );
    }
}