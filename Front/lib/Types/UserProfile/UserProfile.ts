import { Contact } from '../Contact';
import IUserProfile from "@/lib/Types/UserProfile/IUserProfile";
export default class UserProfile implements IUserProfile {
    public Id: string;
    Avatar?: Blob | undefined;
    City: string;
    Country: string;
    Contacts: Contact[];
    FirstName?: string;
    LastName?: string;
    constructor(id: string, city: string, country: string, contact: Contact[], avatar?: Blob | undefined) {
        this.Id = id;
        this.City = city;
        this.Country = country;
        this.Contacts = contact;
        this.Avatar = avatar;
    }
}