import ContactTypes from "./ContactTypes";

export class Contact {
    Link: string;
    TypeOfContact: number;
    DisplayName: string;
    IsVerified: boolean;

    constructor(link: string, typeOfContact: string, displayName: string, isVerified: boolean) {
        this.Link = link;

        this.TypeOfContact = ContactTypes.findIndex((value) => value === typeOfContact);
        this.DisplayName = displayName;
        this.IsVerified = isVerified;
    }
}