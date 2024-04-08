import Cookies from "js-cookie";
import React from "react";
import {LogOut} from "@/Components/LogOut";
import { GetOwnProfile } from "@/lib/Profile";
import ProfileComponent from "@/Components/ProfileComponent";


export default async function UserProfile() {
    const token = Cookies.get('token')
    const profile = await GetOwnProfile(token ?? '');
    

    return(
        <div>
            <p>MyProfile</p>
            <LogOut/>
            <ProfileComponent profile={profile}/>
        </div>
    )
}