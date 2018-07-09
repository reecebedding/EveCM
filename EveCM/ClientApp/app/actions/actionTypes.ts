import *  as bulletinActions from './bulletinActions';
import *  as userActions from './userActions';
import * as adminActions from './adminActions';

//Use Tagged union types to append all known types to this
export type BulletinActions =
    bulletinActions.LoadBulletinSuccessAction
    & bulletinActions.SaveBulletinSuccessAction
    & bulletinActions.RemoveBulletinSuccessAction;

export type UserActions = userActions.LoadUserSuccessAction;

export type AdminPermissionsActions =
    adminActions.LoadAdminPermissionSuccessAction
    & adminActions.LoadRoleInformationSuccessAction
    & adminActions.RemoveMemberFromRoleSuccessAction
    & adminActions.AddMemberToRoleSuccessAction;

