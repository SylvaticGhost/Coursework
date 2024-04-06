import UserProfileToAddDto from "@/lib/Types/UserProfile/UserProfileToAddDto";
import { use } from "react";
import { Contact } from "./Types/Contact";


export async function CreateProfile(profile: UserProfileToAddDto, token: string) {
    console.log('using token: ' + token)
    
    const jsonProfile = profile.toJson
    console.log('Profile to send: ' + jsonProfile)
    
    const response = await fetch('http://localhost:5239/Profile/CreateUserProfile', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json',
            'Accept': '*/*',
            'AcceptEncoding': 'gzip, deflate, br',
            'Connection': 'keep-alive'},
            
        body: jsonProfile
    });

    if(response.ok) {
        console.log('Profile created');
        await AddContact(profile.Contacts, token);
        return;
    }
    else 
        console.log('Profile not created');
}


async function AddContact(contacts: Contact[], token:string) {
    
    console.log('Contacts to send: ' + contacts);
    const content = JSON.stringify(contacts[0]);
    console.log('Content to send: ' + content)
    const response = await fetch('http://localhost:5239/Profile/AddContact', {
        method: 'POST',
        headers: {
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json',
            'Accept': '*/*',
            'AcceptEncoding': 'gzip, deflate, br',
            'Connection': 'keep-alive'},
            
        body: content
    });
    console.log(response)
}