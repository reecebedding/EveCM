import *  as bulletinActions from './bulletinActions';
import *  as userActions from './userActions';

//Use Tagged union types to append all known types to this
export type ActionTypes =
    bulletinActions.LoadBulletinSuccessAction
    & userActions.LoadUserSuccessAction
    & bulletinActions.SaveBulletinSuccessAction;
