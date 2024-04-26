'use client'

import Cookies from "js-cookie";
import React, {useEffect, useState} from "react";
import {LogOut} from "@/Components/LogOut";
import { getOwnProfile } from "@/lib/Profile";
import ProfileComponent from "@/Components/ProfileComponent";
// @ts-ignore
import UserProfile from "@/lib/Types/UserProfile/UserProfile";
import MainHead from "@/Components/MainHead";
import ToMainPageBtn from "@/Components/ToMainPageBtn";
import FailedToLoadProfileComponent from "@/app/Profile/Components/FailedToLoadProfileComponent";

const url : string = 'http://localhost:3000'

export default function UserProfile() {
    const [profile, setProfile] = useState<UserProfile | undefined>(undefined);
    const token = Cookies.get('token')

    useEffect(() => {
        async function fetchProfile() {
            if (token) {
                const profileData = await getOwnProfile(token);
                
                setProfile(profileData);
            }
        }

        fetchProfile().then(r => {});
    }, [token]);

    if(!token)
        return <div className="center-content">Not logged in</div>

    if (!profile) 
        return <FailedToLoadProfileComponent/>
    

    return(
        <div className="flex justify-center my-10">
            <div className="mx-10 mt-8 pr-10 text-xl text-purple-500 text-center font-semibold">
                <LogOut/>
                <p className="mt-10">
                    <a href={url + '/Profile/MyProfile/EditProfile'}>Edit</a>
                </p>
                <br/>
                <p>
                    <a href={url + '/Profile/MyProfile/Settings'}>Settings</a>
                </p>
            </div>
            <div className="text-center">
                <MainHead text="My Profile"/>

                <ProfileComponent profile={profile}/>
            </div>
        </div>
    )
}