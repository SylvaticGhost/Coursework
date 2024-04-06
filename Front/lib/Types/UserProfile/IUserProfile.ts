import { Contact } from '../Contact';

export default interface IUserProfile { 
    Avatar?: Blob;
    City: string;
    Country: string;
    Contacts: Contact[];
}