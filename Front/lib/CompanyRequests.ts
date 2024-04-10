import CompanyToAddDto from "@/lib/Types/Companies/CompanyToAddDto";
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