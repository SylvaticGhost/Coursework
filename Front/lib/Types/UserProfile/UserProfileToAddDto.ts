import { Contact } from '../Contact';
import IUserProfile from './IUserProfile';

export default class UserProfileToAddDto implements IUserProfile {

    private _token: string;
    Avatar?: Blob | undefined;
    City: string;
    Country: string;
    Contact: Contact[];

    constructor(token: string | undefined, city: string, country: string, contact: Contact[], avatar?: Blob | undefined) {
        if(!token)
            throw new Error('Token is required');
        
        this._token = token;
        this.City = city;
        this.Country = country;
        this.Contact = contact;
        this.Avatar = avatar;
    }
    
    //for debugging
    toString(): string {
        return `Token: ${this._token}\n, City: ${this.City}\n, Country: ${this.Country}\n, Contacts: ${this.Contact}\n, Avatar: ${this.Avatar}`;
    }
    
}