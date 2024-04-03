'use client'

import {useState} from "react";
import Cookies from "js-cookie";
import UserProfileToAddDto from "@/lib/Types/UserProfile/UserProfileToAddDto";
import {Contact} from "@/lib/Types/Contact";

export default function CreateProfile() { 
    const token = localStorage.get('token')
    
    if(!token) {
        window.location.href = 'http://localhost:3000/Auth/login'
        return 
    }
    
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
                                           onChange={e =>
                                               e.target.value.replace(/[^a-zA-Zа-яёА-ЯЁіІєЄїЇґҐ\s]/g, '')}
                                    />
                                </div>
                                <div className="py-2">
                                    <input type="text" placeholder="City"
                                           className="border-2 rounded-md py-1"
                                           value={city}
                                           onChange={e =>
                                               e.target.value.replace(/[^a-zA-Zа-яёА-ЯЁіІєЄїЇґҐ\s]/g, '')}
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
                                                {GetContactsType().map((type) => (
                                                    <option value={type}>{type}</option>
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
        let c : Contact[] = []

        const avatarInput = document.getElementById('input') as HTMLInputElement
        let avatar: Blob | undefined ;
        if (avatarInput.files && avatarInput.files.length > 0) {
            avatar = avatarInput.files[0];
        }
        
        
        const profile : UserProfileToAddDto = new UserProfileToAddDto(token, city, country, c, avatar)
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