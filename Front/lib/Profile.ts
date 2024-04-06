import UserProfileToAddDto from "@/lib/Types/UserProfile/UserProfileToAddDto";
import {Contact} from "./Types/Contact";
import UserProfile from "@/lib/Types/UserProfile/UserProfile";


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
      return new UserProfile(data.id, data.city, data.country, data.contacts, data.avatar, data.firstName, data.lastName);
  } else {
    console.log('Profile not found');
  }
}


export async function GetOwnProfile(token: string) {
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
    return new UserProfile(data.id, data.city, data.country, data.contacts, data.avatar, data.firstName, data.lastName);
  } else {
    console.log('Profile not found');
  }

}