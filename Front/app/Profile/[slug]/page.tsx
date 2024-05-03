'use client'

import UserProfile from "@/lib/Types/UserProfile/UserProfile";
import React, {useEffect, useState} from "react";
import {LoadingComponent} from "@/Components/LoadingComponent";
import {getUserProfile} from "@/lib/Profile";
import MainHead from "@/Components/MainHead";
import BackButtonComponent from "@/Components/BackButtonComponent";
import ToMainPageBtn from "@/Components/ToMainPageBtn";
import ProfileComponent from "@/Components/ProfileComponent";

export default function ProfilePage ( {params: {slug}}: {params: {slug: string}}) {
    const [profile, setProfile] = useState<UserProfile | undefined>(undefined);
    
    const [loading, setLoading] = useState<boolean>(true);
    
    useEffect(() => { 
        getUserProfile(slug).then(profile => setProfile(profile));
        setLoading(false);
    })
    
    if(loading)
        return <LoadingComponent/>
    
    if (!profile)
        return <div className="center-content">
                <MainHead text="Profile not found"/>
                <p className="my-2">
                    <BackButtonComponent/>
                </p>
                <ToMainPageBtn/>
                </div>
    
    return ( 
        <div className="center-content">
            <ProfileComponent profile={profile}/>
        </div>
    )
}