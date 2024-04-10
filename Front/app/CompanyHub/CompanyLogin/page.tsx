'use client'

import {useState} from "react";

export default function CompanyLogin() {
    const [companyId, setCompanyId] = useState<string>('');
    const [companyToken, setCompanyToken] = useState<string>('');
    
    return (
        <div className="flex justify-center my-8">
            <div className="flex-row">
                <div className="flex justify-center items-center">
                    <h1 className="text-2xl font-semibold text-purple-600 mb-2">Company Login</h1>
                </div>
                <div className="flex justify-center items-center">
                    <input className="border-2 rounded-md py-1 my-2"
                           value={companyId}
                           placeholder=" company id"
                           onChange={e => setCompanyId(e.target.value)}/>
                </div>
                <div className="flex justify-center items-center">
                    <input className="border-2 rounded-md py-1 my-2"
                           value={companyToken}
                           placeholder=" company token"
                           onChange={e => setCompanyToken(e.target.value)}/>
                </div>
                <div className="my-4 flex justify-center items-center">
                    <button className="button-press rounded-2xl font-semibold p-3 text-lg bg-fuchsia-600 text-white shadow-xl
                        hover:bg-fuchsia-700">
                        Login
                    </button>

                </div>
            </div>
        </div>
    )
}

function onLogin() {
    
}