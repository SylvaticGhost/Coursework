'use client';

import Cookies from "js-cookie";
import React from "react";
import {LogOut} from "@/Components/LogOut";
import { GetOwnProfile } from "@/lib/Profile";


export default function UserProfile() { 
    const token = Cookies.get('token')

    if(!token) {
        window.location.href = 'http://localhost:3000/Auth/login'
        return 
    }

    const profile = GetOwnProfile(token);
    
    return(
        <div>
            <p>MyProfile</p>
            <LogOut/>
        </div>
    )
}

