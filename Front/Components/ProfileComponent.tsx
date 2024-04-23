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
    
    console.log(profile)
    
    if(profile === null || profile === undefined) {
        //window.location.href = 'http://localhost:3000/Auth/login'
        console.log('profile is null')
        return
    }
    //profile.Avatar ? URL.createObjectURL(profile.Avatar) : undefined
    return (
        <div className="m-5 flex justify-center">
            <div className="m-3 p-2">
                
                <img src="/Media/UserDefaultAvatar.png"
                     alt="Avatar"
                     className="rounded-full h-32 w-32"/>
            </div>
            <div className="m-3 p-3">
                <p className="rounded-2xl bg-gray-200 text-xl flex text-center m-3 p-3">
                    {profile.FirstName + ' ' + profile.LastName}
                </p>
                <p className="rounded-2xl bg-gray-200 flex text-center m-3 p-2">
                    {profile.Country + ', ' + profile.City}
                </p>
                <p className="rounded-2xl bg-gray-200 flex text-center m-3 p-2">
                    About me: {profile.About ? profile.About : 'No information'}
                </p>
            </div>
        </div>
    )
}