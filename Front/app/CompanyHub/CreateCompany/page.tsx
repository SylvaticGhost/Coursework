'use client'

import {useState} from "react";
import CompanyToAddDto from "@/lib/Types/Companies/CompanyToAddDto";

export default function CreateCompany() {
    const [companyName, setCompanyName] = useState('');
    const [companyAddress, setCompanyAddress] = useState('');
    const [email, setEmail] = useState('');
    const [phone, setPhone] = useState('');
    const [description, setDescription] = useState('');
    const [website, setWebsite] = useState('');
    const [industry, setIndustry] = useState('');
    const [password, setPassword] = useState('');
    
    const [inputed, setInputed] = useState(false);
    
    const onCreate = () => {
        setInputed(true)
        const company = 
            new CompanyToAddDto(companyName, email, phone, description, companyAddress, website, undefined, industry)
    }
    
    return (
        !inputed ? (
        <div className="flex justify-center py-3">
            <div className="flex-row justify-center items-center">
                <div>
                    <input type="text" placeholder="company name"
                           className="border-2 rounded-md py-1 m-2"
                           value={companyName}
                           onChange={e => setCompanyName(e.target.value)}/>
                    <br/>
                </div>
                <div>
                    <input type="text" placeholder="company address"
                           className="border-2 rounded-md py-1 m-2"
                           value={companyAddress}
                           onChange={e => setCompanyAddress(e.target.value)}/>
                    <br/>
                </div>
                <div>
                    <input type="text" placeholder="email"
                           className="border-2 rounded-md py-1 m-2"
                           value={email}
                           onChange={e => setEmail(e.target.value)}/>
                    <br/>
                </div>
                <div>
                    <input type="text" placeholder="phone"
                           className="border-2 rounded-md py-1 m-2"
                           value={phone}
                           onChange={e => setPhone(e.target.value)}/>
                    <br/>
                </div>
                <div>
                    <input type="text" placeholder="description"
                           className="border-2 rounded-md py-1 m-2"
                           value={description}
                           onChange={e => setDescription(e.target.value)}/>
                    <br/>
                </div>
                <div>
                    <input type="text" placeholder="website"
                           className="border-2 rounded-md py-1 m-2"
                           value={website}
                           onChange={e => setWebsite(e.target.value)}/>
                    <br/>
                </div>
                <div>
                    <input type="text" placeholder="industry"
                           className="border-2 rounded-md py-1 m-2"
                           value={industry}
                           onChange={e => e.target.value}/>
                    <br/>
                </div>
                <div className="text-center pb-2">
                    <label>password to CompanyHub</label>
                    <br/>
                    <input
                        className="border-2 rounded-md py-1 m-2"
                        type="password" 
                        placeholder="password" 
                        value={password} 
                        onChange={e => setPassword(e.target.value)}/>
                </div>
                <div className="flex justify-center items-center">
                    <button
                        className="rounded-xl font-semibold p-2 text-lg bg-fuchsia-600 text-white">
                        Create
                    </button>
                </div>
    
    
            </div>
    </div>) :
        <div>
            <p>Created</p>
            <br/>
            <a href="http://localhost:3000">To main page</a>
        </div>
    
    )
}
