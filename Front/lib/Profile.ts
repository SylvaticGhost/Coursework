import UserProfileToAddDto from "@/lib/Types/UserProfile/UserProfileToAddDto";
import {Contact} from "./Types/Contact";
import UserProfile from "@/lib/Types/UserProfile/UserProfile";
import {UserProfileToUpdateDto} from "@/lib/Types/UserProfile/UserProfileToUpdateDto";
import {ID} from "postcss-selector-parser";


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


export async function AddContact(contacts: Contact[], token:string) {
    
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

export async function GetProfile(id: string) {
  const response = await fetch(`http://localhost:5239/Profile/GetProfile?id=${id}`, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      'Accept': '*/*',
      'AcceptEncoding': 'gzip, deflate, br',
      'Connection': 'keep-alive'},
    },
  );

  if (response.ok) {
    const data = await response.json();
      return new UserProfile(data.userId, data.city, data.country, data.contacts, data.avatar, data.firstName, data.lastName);
  } else {
    console.log('Profile not found');
  }
}


export async function getOwnProfile(token: string) {
  const response = await fetch('http://localhost:5239/Profile/GetOwnProfile', {
    method: 'GET',
    headers: {
      'Authorization': 'Bearer ' + token,
      'Content-Type': 'application/json',
      'Accept': '*/*',
      'AcceptEncoding': 'gzip, deflate, br',
      'Connection': 'keep-alive'},
     },
    );

  if (response.ok) {
    const data = await response.json();
      const profile = new UserProfile(data.userId, data.city, data.country, data.contacts, data.avatar, data.firstName, data.lastName, data.about);
      console.log(profile.toString());
    return profile;
  } else {
    console.log('Profile not found');
    return undefined;
  }
}


export async function updateProfile(profile: UserProfileToUpdateDto, token: string) : Promise<boolean> {
    const jsonProfile = JSON.stringify(profile);
    
    const response = await fetch('http://localhost:5239/Profile/UpdateUserProfile', {
        method: 'POST',
        headers: { 
            'Authorization': 'Bearer ' + token,
            'Content-Type': 'application/json',
            'Accept': '*/*',
            'AcceptEncoding': 'gzip, deflate, br',
            'Connection': 'keep-alive'},
        body: jsonProfile
        },
    );
    
    if(response.ok) {
        console.log('Profile updated');
        return true;
    }
    else {
        console.log('Profile not updated');
        return false;
    }
}