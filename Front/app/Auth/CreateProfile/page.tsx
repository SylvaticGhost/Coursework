'use client'

import {useState} from "react";
import Cookies from "js-cookie";
import UserProfileToAddDto from "@/lib/Types/UserProfile/UserProfileToAddDto";
import {Contact} from "@/lib/Types/Contact";

export default function CreateProfile() { 
    const token = localStorage.getItem('token')
    
    console.log('token in create profile: ' + token)
    
    if(!token) {
        window.location.href = 'http://localhost:3000/Auth/login'
        return 
    }
    
    console.log(token)
    
    const [error, setError] = useState('')
    const [country, setCountry] = useState('')
    const [city, setCity] = useState('')
    const [contacts, setContacts] = useState('')
    const [contactType, setContactType] = useState('')
    
    return(
        <div>
            <div className="flex h-screen justify-center items-center m-4">
                <div className="flex-row">
                    <div className="register__container p-4 rounded-2xl border-2 border-fuchsia-600 m-2">
                        <h2 className="font-semibold text-purple-500 flex justify-center items-center mb-1">
                            Create profile
                        </h2>
                        <div className="register__container__form">
                            <form className="flex-column">
                                <div>
                                    <label className="text-fuchsia-600">Upload avatar</label><br/>
                                    <input type="file" id="input" accept="image/*"
                                           className="p-1 "
                                           title="Upload profile picture"
                                    />
                                </div>
                                <div className="py-2">
                                    <input type="text" placeholder="Country"
                                           className="border-2 rounded-md py-1"
                                           value={country}
                                           onChange={e => {
                                               const newValue = e.target.value.replace(/[^a-zA-Zа-яёА-ЯЁіІєЄїЇґҐ\s]/g, '');
                                               setCountry(newValue);
                                           }}
                                    />
                                </div>
                                <div className="py-2">
                                    <input type="text" placeholder="City"
                                           className="border-2 rounded-md py-1"
                                           value={city}
                                           onChange={e => {
                                               const newValue = e.target.value.replace(/[^a-zA-Zа-яёА-ЯЁіІєЄїЇґҐ\s]/g, '');
                                               setCity(newValue);
                                           }}
                                    />
                                </div>
                                <div>
                                    <label className="text-fuchsia-600">Contacts</label><br/>
                                    <div className="flex-column">
                                        <div className="flex-row">
                                            <input type="text" placeholder="Contact"
                                                   className="border-2 rounded-md py-1"
                                            />
                                            <select className="border-2 rounded-md py-1 ml-2" onSelect={e => setContactType(e.type)}>
                                                {GetContactsType().map((type, index) => (
                                                    <option key={index} value={type}>{type}</option>
                                                ))}
                                            </select>
                                        </div>
                                    </div>
                                </div>

                            </form>
                            <div className="flex justify-center">
                                <button type="submit"
                                        className="bg-fuchsia-700 rounded-md p-2 text-amber-50 font-semibold mt-4 flex justify-center 
                                        hover:bg-fuchsia-800 transition duration-200 active:scale-90"
                                        onClick={OnCreate}
                                >
                                    Create
                                </button>
                            </div>
                        </div>
                    </div>
                    <div className="pt-3 flex justify-center items-center">
                        {error && <div className="text-red-500">{error}</div>}
                    </div>
                </div>

            </div>
        </div>
    )

    function OnCreate() {
        const contact : Contact = { Link: contacts, TypeOfContact: contactType, IsVisible: true, DisplayName: contacts }
        
        const c : Contact[] = [contact]
        
        const avatarInput = document.getElementById('input') as HTMLInputElement
        let avatar: Blob | undefined ;
        if (avatarInput.files && avatarInput.files.length > 0) {
            avatar = avatarInput.files[0];
        }
        
        if(!token)
            throw new Error('Token is not defined')
        
        const profile : UserProfileToAddDto = new UserProfileToAddDto(token, city, country, c, avatar)
        
        console.log('Profile: \n' + profile.toString())
    }
}





function GetContactsType() {
    return [
        "Email",
        "PhoneNumber",
        "Facebook",
        "Instagram",
        "LinkedIn",
        "Twitter",
        "Telegram",
        "WhatsApp",
        "Viber",
        "Skype",
        "Discord",
        "GitHub",
        "Other"
    ] as const;
}