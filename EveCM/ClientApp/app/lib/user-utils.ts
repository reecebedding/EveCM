import { IUser } from '../components/common/userDetails/interfaces/Interfaces';

export function isInRole(role: string, user: IUser): boolean {
    return user.roles.some(x => x.toUpperCase() === role.toUpperCase());
}