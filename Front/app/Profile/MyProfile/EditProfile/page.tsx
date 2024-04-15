'use client'

import {useState} from "react";
import MainHead from "@/Components/MainHead";

export default function EditProfilePage() {
    const [firstName, setFirstName] = useState<string>('');
    const [lastName, setLastName] = useState<string>('');
    
    return (
        <div>
            <MainHead text={"Edit Profile"}/>
            <div>
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
        </div>
    )
}