import CompanyToAddDto from "../../lib/Types/Companies/CompanyToAddDto";

export default async function CreateCompany (company: CompanyToAddDto) {
    const response = await fetch('http://localhost:5240/Company/CreateCompmany', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': '*/*',
            'AcceptEncoding': 'gzip, deflate, br',
        },
        body: JSON.stringify(company)
    })
}