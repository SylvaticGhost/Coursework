'use client'

import React, { useEffect, useState } from "react";
import MainHead from "@/Components/MainHead";
import Cookies from "js-cookie";
import { changePassword } from "@/lib/Requests/Profile/UpdatingProfile"
import { InternalErrorComponent } from "@/Components/InternalErrorComponent";
import BackButtonComponent from "@/Components/BackButtonComponent";

export function ChangePasswordPage() {
    const [password, setPassword] = useState('');
    const [passwordConfiramtion, setPasswordConfirmation] = useState('')

    const [error, setError] = useState<string | null>(null)
    const [showPassword, setShowPassword] = useState(false)
    const [passwordChanged, setPasswordChanged] = useState(false)
    const [internalError, serInternalError] = useState(false)

    const token: string | undefined = Cookies.get('token');

    //TODO: validation
    async function sendUpdateRequest() {
        if(!password) {
            setError('Password field is required')
            return
        }

        if(!passwordConfiramtion){
            setError('Confirm your password')
            return;
        }

        if(password != passwordConfiramtion){
            setError('Password and confiramtion are not the same')
            return;
        }
        
        setError(null)

        changePassword(password, token ?? '')
        .then(result => setPasswordChanged(result))
        .catch(e => serInternalError(true))
    }

    if(internalError)
        return <InternalErrorComponent/>
    
    if(!passwordChanged)
        return (
        <div className="center-content">
            <MainHead text="Update password" />
            <div>
                <div className="mt-4">
                    <input type={showPassword ? 'text' : 'password'}
                        value={password}
                        className="default-text-input"
                        onChange={e => setPassword(e.target.value)} />
                    <input type={showPassword ? 'text' : 'password'}
                        value={passwordConfiramtion}
                        className="default-text-input mt-3"
                        onChange={e => setPasswordConfirmation(e.target.value)} />
                </div>
                <button className="mt-4 purple-text"
                    onClick={e => setShowPassword(!showPassword)}>
                    {
                        showPassword ? 'Hide password' : 'Show password'
                    }
                </button>
            </div>
            {
                error ? 
                <div className="mt-3">
                    <p className="error-text">{error}</p>
                </div>
                 : null
            }
            <div className="mt-5">
                <button className="default-purple-button"
                onChange={e => sendUpdateRequest()}>
                    Change
                </button>
            </div>

        </div>
    )
    else
        return (
            <div className="center-content">
                <MainHead text="Password updated"/>
                <p className="text-xl my-4 text-center">Don't forgot new one</p>
                <BackButtonComponent/>
            </div>
        )
}