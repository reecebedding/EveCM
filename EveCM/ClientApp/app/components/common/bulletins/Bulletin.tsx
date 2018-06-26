import * as React from 'react';
import { IBulletin } from './interfaces/Interfaces';

const Bulletin: React.SFC < { bulletin: IBulletin } > = (props) => {
    return (
        <div className="card-body">
            <h5 className="card-title">{props.bulletin.title}</h5>
            <p className="card-text">{props.bulletin.content}</p>
            <footer className="blockquote-footer">
                <img src={props.bulletin.authorCharacter.avatarUrl} className="mr-2"/>
                {props.bulletin.authorCharacter.userName} - {new Date(props.bulletin.date).toLocaleDateString()}
            </footer>
        </div>
    );
}

export default Bulletin;