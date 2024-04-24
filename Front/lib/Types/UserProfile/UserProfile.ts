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
    About?: string;
    constructor(id: string, city: string, country: string, contact: Contact[], avatar?: Blob | undefined, firstName?: string, lastName?: string, about?: string) {
        if (!id)
            console.log('Id is null');
        
        this.Id = id;
        this.City = city;
        this.Country = country;
        this.Contacts = contact;
        this.Avatar = avatar;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.About = about;
    }
    
    
    public toString(): string { 
        let stringBuilder : string[] = [];
        stringBuilder.push('Id: ' + this.Id);
        stringBuilder.push('City: ' + this.City);
        stringBuilder.push('Country: ' + this.Country);
        stringBuilder.push('Contacts: ' + this.Contacts);
        stringBuilder.push('Avatar: ' + this.Avatar ? 'Avatar is not null' : 'Avatar is null');
        stringBuilder.push('FirstName: ' + this.FirstName);
        stringBuilder.push('LastName: ' + this.LastName);
        stringBuilder.push('About: ' + this.About);
        return stringBuilder.join('\n');
    }
    
    
    
}