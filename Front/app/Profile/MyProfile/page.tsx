﻿'use client'

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
import UnauthorizedException from "@/lib/Errors/UnauthorizedException";
import NotLoggedComponent from "@/app/Profile/Components/NotLoggedComponent";
import {LoadingComponent} from "@/Components/LoadingComponent";

const url : string = 'http://localhost:3000'

export default function UserProfile() {
    const [profile, setProfile] = useState<UserProfile | undefined>(undefined);
    const [authorized, setAuthorized] = useState<boolean>(true);
    
    const [loading, setLoading] = useState<boolean>(true);
    
    const token = Cookies.get('token')

    useEffect(() => {
        async function fetchProfile() {
            if (token) {
                try {
                    const profileData = await getOwnProfile(token);
                    setProfile(profileData);
                    setLoading(false);
                }
                catch(exception: any) {
                    if(exception instanceof UnauthorizedException) {
                        setAuthorized(false);
                        setLoading(false);
                    }
                }
            }
            setLoading(false);
        }

        fetchProfile().then(r => {});
    }, [token]);
    
    if(loading)
        return <LoadingComponent/>

    if(!token || !authorized)
        return <NotLoggedComponent/>

    if (!profile) 
        return (
            <div className="center-content">
                <FailedToLoadProfileComponent/>
                <a href={url + '/Auth/CreateProfile'} className="my-2 font-semibold text-purple-500">If you haven't created create now</a>
            </div>
        )
    

    return(
        <div className="flex justify-center my-10 min-h-55vh">
            <div className="mx-10 mt-8 pr-10 text-xl text-purple-500 text-center font-semibold">
                <LogOut/>
                <p className="mt-10">
                    <a href={url + '/Profile/MyProfile/EditProfile'}>Edit</a>
                </p>
                <br/>
                <p>
                    <a href={url + '/Profile/MyProfile/Settings'}>Settings</a>
                </p>
                <p className="m-10">
                    <a href={url + '/Profile/' + profile.Id}>Public view</a>
                </p>
            </div>
            <div className="text-center">
                <MainHead text="My Profile"/>

                <ProfileComponent profile={profile}/>
            </div>
        </div>
    )
}