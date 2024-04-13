'use client'

import Cookies from "js-cookie";
import React, {useEffect, useState} from "react";
import {LogOut} from "@/Components/LogOut";
import { GetOwnProfile } from "@/lib/Profile";
import ProfileComponent from "@/Components/ProfileComponent";
// @ts-ignore
import UserProfile from "@/lib/Types/UserProfile/UserProfile";
import MainHead from "@/Components/MainHead";


export default function UserProfile() {
    const [profile, setProfile] = useState<UserProfile | undefined>(undefined);
    const token = Cookies.get('token')

    useEffect(() => {
        async function fetchProfile() {
            if (token) {
                const profileData = await GetOwnProfile(token);
                setProfile(profileData);
            }
        }

        fetchProfile();
    }, [token]);

    if(!token)
        return <div className="center-content">Not logged in</div>

    return(
        <div className="flex justify-center my-10">
            <div className="mx-10 mt-8 pr-10 text-xl text-purple-500 text-center font-semibold">
                <LogOut/>
                <p className="mt-10">
                    <a>Edit</a>
                </p>
                <br/>
                <p>
                    <a>Settings</a>
                </p>
            </div>
            <div className="text-center">
                <MainHead text="My Profile"/>

                <ProfileComponent profile={profile}/>
            </div>
        </div>
    )
}