import { Role } from './role';

export class User {
    id?: string;
    username?: string;
    email?: string;
    role?: Role;
    token?: string;
    expires_in?: string;
}