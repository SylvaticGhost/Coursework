import UserProfileToAddDto from "@/lib/Types/UserProfile/UserProfileToAddDto";
import { use } from "react";

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
        return;
    }
    else 
        console.log('Profile not created');
}