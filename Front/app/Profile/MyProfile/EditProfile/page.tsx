'use client'

import React, {useEffect, useState} from "react";
import MainHead from "@/Components/MainHead";
import UserProfile from "@/lib/Types/UserProfile/UserProfile";
import Cookies from "js-cookie";
import {getOwnProfile, updateProfile} from "@/lib/Profile";
import ToMainPageBtn from "@/Components/ToMainPageBtn";
import FailedToLoadProfileComponent from "@/app/Profile/Components/FailedToLoadProfileComponent";
import {UserProfileToUpdateDto} from "@/lib/Types/UserProfile/UserProfileToUpdateDto";

export default function EditProfilePage() {
    const [firstName, setFirstName] = useState<string>('');
    const [lastName, setLastName] = useState<string>('');
    const [about, setAbout] = useState<string>('');
    const [city, setCity] = useState<string>('');
    const [country, setCountry] = useState<string>('');
    
    const [profile, setProfile] = useState<UserProfile | undefined>(undefined);
    
    const [updated, setUpdated] = useState<boolean>(false);
    const [errorOnUpdate, setErrorOnUpdate] = useState<boolean>(false);

    const token = Cookies.get('token')

    useEffect(() => {
        (async () => {
            if (token && !profile) {
                const profileData = await getOwnProfile(token);
                setProfile(profileData);
                if(profileData) {
                    setFirstName(profileData.FirstName || '');
                    setLastName(profileData.LastName || '');
                    setAbout(profileData.About || '');
                    setCity(profileData.City);
                    setCountry(profileData.Country);
                }
            }
        })(); }, [token, profile]);
    
    if(!token)
        return <div className="center-content">
                <a>Not logged in</a>
                <a href="/login">Login</a>
                </div>

    if (!profile)
        return <FailedToLoadProfileComponent/>
    
    const updateProfileBtn = async () => { 
        const profileToUpdate : UserProfileToUpdateDto = { 
            FirstName: firstName,
            LastName: lastName,
            About: about,
            City: city,
            Country: country,
            Contacts: profile.Contacts,
            Avatar: profile.Avatar
        }
        console.log(profileToUpdate);
        const result: boolean = await updateProfile(profileToUpdate, token);
        
        if (result)
            setUpdated(true);
        else
            setErrorOnUpdate(true);
    }
    
    if (errorOnUpdate) { 
        return (<div className="center-content"> 
            <MainHead text={"Failed to update profile"}/>
            <a href="/Profile/MyProfile" className="my-3">Back to profile</a>
            <button
                onClick={() => setErrorOnUpdate(false)}
                className="default-purple-button">
                Try again
            </button>
            <p className="my-3">
                <ToMainPageBtn/>
            </p>
        </div>
        );
    }
    
    if (updated) {
        return <div className="center-content">
            <MainHead text={"Profile updated"}/>
            <a href="/Profile/MyProfile" className="my-3 text-xl font-bold text-purple-500">Back to profile</a>
            <button
                onClick={() => setUpdated(false)}
                className="default-purple-button">
                Edit again
            </button>
            <p className="my-3">
                <ToMainPageBtn/>
            </p>
        </div>
    }

    // @ts-ignore
    return (
        <div className="center-content">
            <MainHead text={"Edit Profile"}/>
            <div className="my-3">
                <input
                    className="default-text-input"
                    placeholder="First name"
                    value={firstName}
                    onChange={e => setFirstName(e.target.value)}
                />
            </div>
            <div>
                <input
                    className="default-text-input"
                    placeholder="Last name"
                    value={lastName}
                    onChange={e => setLastName(e.target.value)}
                />
            </div>
            <div className="my-3">
                <input
                    className="default-text-input"
                    placeholder="City"
                    value={city}
                    onChange={e => setCity(e.target.value)}
                />
            </div>
            <div className="">
                <input
                    className="default-text-input"
                    placeholder="Country"
                    value={country}
                    onChange={e => setCountry(e.target.value)}
                />
            </div>
            <div className="my-3">
                <textarea 
                placeholder=" About"
                value={about}
                onChange={e => setAbout(e.target.value)}
                className="default-text-input w-2/12 h-4"/> 
            </div>
            <div className="mt-2">
                <button className="default-purple-button"
                onClick={updateProfileBtn}>Save</button>
            </div>
            <div className="my-3">
                <ToMainPageBtn/>
            </div>
            <div>
                <a className="text-xl font-bold text-purple-500" href="/Profile/MyProfile">Back to profile</a>
            </div>

        </div>
    )
}