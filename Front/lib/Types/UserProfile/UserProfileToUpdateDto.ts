import {Contact} from "@/lib/Types/Contact";

export type UserProfileToUpdateDto = { 
    FirstName: string;
    LastName: string;
    About: string;
    City: string;
    Country: string;
    Contacts: Contact[];
    Avatar: Blob | undefined;
}