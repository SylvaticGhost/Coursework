﻿import { Contact } from '../Contact';
import IUserProfile from "@/lib/Types/UserProfile/IUserProfile";
export default class UserProfile implements IUserProfile {
    public Id: string;
    Avatar?: Blob | undefined;
    City: string;
    Country: string;
    Contact: Contact[];
    constructor(id: string, city: string, country: string, contact: Contact[]) {
        this.Id = id;
        this.City = city;
        this.Country = country;
        this.Contact = contact;
    }
}