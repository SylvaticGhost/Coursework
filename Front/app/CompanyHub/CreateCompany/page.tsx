'use client'

import {useState} from "react";
import CompanyToAddDto from "@/lib/Types/Companies/CompanyToAddDto";
import {CreateCompanyReq} from "@/lib/CompanyRequests";

export default function CreateCompany() {
    //region "form fields"
    const [companyName, setCompanyName] = useState('');
    const [companyAddress, setCompanyAddress] = useState('');
    const [email, setEmail] = useState('');
    const [phone, setPhone] = useState('');
    const [description, setDescription] = useState('');
    const [website, setWebsite] = useState('');
    const [industry, setIndustry] = useState('');
    const [acceptTerms, setAcceptTerms] = useState(false);
    //endregion
    
    const [companyId, setCompanyId] = useState<string>('');
    const [companyToken, setCompanyToken] = useState<string>('');
    const [hideCompanyCredentials, setHideCompanyCredentials] = useState<boolean>(true);

    const [copiedCompanyId, setCopiedCompanyId] = useState(false);
    const [copiedCompanyToken, setCopiedCompanyToken] = useState(false);


    //change after pressing the create button and filling the form
    const [inputed, setInputed] = useState(false);
    
    const [error, setError] = useState('');

    const copyToClipboardWithTimeout = (copyFunction: () => void, setCopiedStateFunction: (copied: boolean) => void) => {
        copyFunction();
        setCopiedStateFunction(true);
        setTimeout(() => setCopiedStateFunction(false), 2000);
    };
    
    const onCreate = async () => {
        if(!acceptTerms) { 
            setError('You must accept terms')
            return
        }
        setError('')
        const company = 
            new CompanyToAddDto(companyName, email, phone, description, companyAddress, website, undefined, industry)
        
        validateCompany(company);
        
        try {
            const result = await CreateCompanyReq(company);
            setInputed(true)
            
            setCompanyId(result.companyId)
            setCompanyToken(result.key)
        }
        catch {
            setError('unable to create company')
        }
    }
    
    // @ts-ignore
    return (
        !inputed ? (
        <div className="flex justify-center py-6">
            <div className="flex-row justify-center items-center text-xl">
                <h2 className="py-3 text-center font-bold text-2xl text-purple-500">
                    Create your company hub
                </h2>
                <div>
                    <input type="text" placeholder=" company name"
                           className="border-2 rounded-md py-1 m-2"
                           value={companyName}
                           onChange={e => setCompanyName(e.target.value)}/>
                    <br/>
                </div>
                <div>
                    <input type="text" placeholder=" company address"
                           className="border-2 rounded-md py-1 m-2"
                           value={companyAddress}
                           onChange={e => setCompanyAddress(e.target.value)}/>
                    <br/>
                </div>
                <div>
                    <input type="text" placeholder=" email"
                           className="border-2 rounded-md py-1 m-2"
                           value={email}
                           onChange={e => setEmail(e.target.value)}/>
                    <br/>
                </div>
                <div>
                    <input type="text" placeholder=" phone"
                           className="border-2 rounded-md py-1 m-2"
                           value={phone}
                           onChange={e => setPhone(e.target.value)}/>
                    <br/>
                </div>
                <div>
                    <input type="text" placeholder=" description"
                           className="border-2 rounded-md py-1 m-2"
                           value={description}
                           onChange={e => setDescription(e.target.value)}/>
                    <br/>
                </div>
                <div>
                    <input type="text" placeholder=" website"
                           className="border-2 rounded-md py-1 m-2"
                           value={website}
                           onChange={e => setWebsite(e.target.value)}/>
                    <br/>
                </div>
                <div>
                    <input type="text" placeholder=" industry"
                           className="border-2 rounded-md py-1 m-2"
                           value={industry}
                           onChange={e => setIndustry(e.target.value)}/>
                    <br/>
                </div>
                <div>
                    <input type="checkbox"
                           className="m-2  large-checkbox text-center align-middle"
                           checked={acceptTerms}
                           onChange={e => setAcceptTerms(e.target.checked)}/>
                    <a>I accept terms</a>
                </div>
                <div className="flex justify-center items-center py-4">
                    <button
                        className="button-press rounded-2xl font-semibold p-3 text-lg bg-fuchsia-600 text-white shadow-xl
                        hover:bg-fuchsia-700"
                        onClick={onCreate}>
                        Create
                    </button>
                </div>
                <div>
                    {error && <p className="text-red-500">{error}</p>}
                </div>
    
            </div>
    </div>) :
        <div className="flex justify-center">
            <div className="flex-col text-center text-lg p-5">
                <div className="pt-5">
                    <p>Company successfully created</p>
                    <button onClick={e => setHideCompanyCredentials(!hideCompanyCredentials)}
                            className="button-press rounded-2xl font-semibold p-3 text-lg bg-fuchsia-600 text-white shadow-xl
                        hover:bg-fuchsia-700 my-5"
                    >
                        {hideCompanyCredentials ? "Show company credentials" : "Hide company credential"}
                    </button>
                    {hideCompanyCredentials ? null : (
                        <div className="py-2">
                            <p>Company Id: {companyId}
                                <button onClick={() => copyToClipboardWithTimeout(
                                    () => navigator.clipboard.writeText(companyId.toString()),
                                    setCopiedCompanyId
                                )}
                                className="p-2 mx-2 bg-gray-200 rounded-xl button-press">
                                    Copy
                                </button>
                                {copiedCompanyId && <span>Copied to clipboard</span>}
                            </p>
                            <p className="mt-3">Company Token: {companyToken}
                                <button onClick={() => copyToClipboardWithTimeout(
                                    () => navigator.clipboard.writeText(companyToken),
                                    setCopiedCompanyToken
                                )}
                                        className="p-2 mx-2 bg-gray-200 rounded-xl button-press">
                                    Copy
                                </button>{copiedCompanyToken && <span>Copied to clipboard</span>}
                            </p>
                        </div>
                    )}
                </div>
                
                <br/>
                <div className="py-3 text-purple-600 text-xl hover:text-purple-700">
                    <a href="http://localhost:3000">To main page</a>
                </div>
            </div>
        </div>
    
    )
    
    function validateCompany(company: CompanyToAddDto) { 
        if(!company.name) {
            setError('Company name is required')
            return false
        }
        if(!company.email) {
            setError('Email is required')
            return false
        }
        if(!company.phoneNumber) {
            setError('Phone is required')
            return false
        }
        if(!company.description) {
            setError('Description is required')
            return false
        }
        if(!company.address) {
            setError('Company address is required')
            return false
        }
        if(!company.webSite) {
            setError('Website is required')
            return false
        }
        if(!company.industry) {
            setError('Industry is required')
            return false
        }
        return true
    }
}

