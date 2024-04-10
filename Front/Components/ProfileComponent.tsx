'use client'

import React from "react";
import UserProfile from "@/lib/Types/UserProfile/UserProfile";
import Cookies from "js-cookie";

type ProfileComponentProps = { 
    profile: UserProfile | undefined    
}

export default function ProfileComponent({profile}: ProfileComponentProps) {

    const token = Cookies.get('token')
    
    if(!token) {
        window.location.href = 'http://localhost:3000/Auth/login'
        return
    }
    
    if(profile === null || profile === undefined) {
        //window.location.href = 'http://localhost:3000/Auth/login'
        console.log('profile is null')
        return
    }
    return (
        <div className="m-5 flex justify-center">
            <div className="m-3 p-2">
                <img src={profile.Avatar ? URL.createObjectURL(profile.Avatar) : undefined}
                     alt="Avatar"
                     className="rounded-full h-32 w-32"/>
            </div>
            <div className="m-3 p-2">
                <p className="rounded-xl bg-gray-200 text-xl flex text-center m-3">
                    {profile.FirstName + ' ' + profile.LastName}
                </p>
            </div>
        </div>
    )
}