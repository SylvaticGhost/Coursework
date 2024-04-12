import CompanyToAddDto from "@/lib/Types/Companies/CompanyToAddDto";
import Cookies from "js-cookie";
const url = 'http://localhost:5240'

export async function CreateCompanyReq(company: CompanyToAddDto) {
    const response = await fetch(url + '/Company/CreateCompany',
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': '*/*',
                'AcceptEncoding': 'gzip, deflate, br',
            },
            body: JSON.stringify(company)
        })
    
    if(!response.ok)
        throw new Error('Failed to create company');
    
    return await response.json();
}


export async function LoginCompanyReq(company: CompanyToLoginDto) {
    const response = await fetch(url + '/Company/CompanyHubLogin',
        {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': '*/*',
                'AcceptEncoding': 'gzip, deflate, br',
            },
            body: JSON.stringify(company)
        })
    console.log('body: ' + JSON.stringify(company))
    if(!response.ok)
        throw new Error('Failed to login company');
    
    const token = await response.text();
    
    Cookies.set('companyToken', token, {expires: 7, secure: process.env.NODE_ENV === 'production'});
}