import { Contact } from '../Contact';
import IUserProfile from './IUserProfile';

export default class UserProfileToAddDto implements IUserProfile {

    private _token: string;
    Avatar?: any;
    City: string;
    Country: string;
    Contacts: Contact[];

    constructor(token: string | undefined, city: string, country: string, contact: Contact[], avatar?: any) {
        if(!token)
            throw new Error('Token is required');
        
        this._token = token;
        this.City = city;
        this.Country = country;
        this.Contacts = contact;
        this.Avatar = avatar;
    }
    
    //for debugging
    toString(): string {
        return `Token: ${this._token}\n, City: ${this.City}\n, Country: ${this.Country}\n, Contacts: ${this.Contacts}\n, Avatar: ${this.Avatar}`;
    }
    
    
    get toJson(){
        if(this.Avatar)
            return JSON.stringify({city: this.City, country: this.Country, contacts: this.Contacts, avatar: this.Avatar, about: " "})
        else
            return JSON.stringify({city: this.City, country: this.Country, contacts: this.Contacts, about: " "})
    }
    
}