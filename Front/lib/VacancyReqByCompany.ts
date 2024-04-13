import {VacancyToAddDto} from "@/lib/Types/Vacancy/VacancyToAddDto";
import Cookies from "js-cookie";
import Vacancy from "@/lib/Types/Vacancy/Vacancy";
import {VacancyToUpdateDto} from "@/lib/Types/Vacancy/VacancyToUpdateDto";

const CompanySvcUrl = 'http://localhost:5240'

export async function CreateVacancyReq(vacancy: VacancyToAddDto) {
    
    const companyToken = Cookies.get('companyToken');
    
    if(!companyToken)
        throw new Error('Company token not found');
    
    const response = fetch(CompanySvcUrl + '/VacancyByCompany/PublishVacancy',
        {
            method: 'POST',
            headers: {
                'Authorization': 'Bearer ' + companyToken,
                'Content-Type': 'application/json',
                'Accept': '*/*',
                'AcceptEncoding': 'gzip, deflate, br',
            },
            body: JSON.stringify(vacancy)
        })
    
    const result = await response;
    
    if(!result.ok)
        throw new Error('Failed to create vacancy');
    
    console.log('Vacancy created');
    alert('Vacancy created')
}

export async function GetCompanyVacancies() {

    const companyToken = Cookies.get('companyToken');
    
    if(!companyToken)
        throw new Error('Company token not found');
    
    const response = fetch(CompanySvcUrl + '/VacancyByCompany/GetCompanyVacancies',
        {
            method: 'GET',
            headers: {
                'Authorization': 'Bearer ' + companyToken,
                'Content-Type': 'application/json',
                'Accept': '*/*',
                'AcceptEncoding': 'gzip, deflate, br',
            }
        })
    
    const result = await response;
    
    if(!result.ok)
        throw new Error('Failed to get vacancies');
    
    const vacanciesGet = await result.json();

    return vacanciesGet.map((vacancy: Vacancy) => {
        return {
            VacancyId: vacancy.vacancyId,
            title: vacancy.title,
            description: vacancy.description,
            salary: vacancy.salary,
            specialization: vacancy.specialization,
            experience: vacancy.experience,
            createdAt: new Date(vacancy.createdAt).toLocaleDateString(),
            updatedAt: vacancy.updatedAt ? new Date(vacancy.updatedAt) : undefined,
            companyShortInfo: {
                companyId: vacancy.companyShortInfo?.companyId,
                name: vacancy.companyShortInfo?.name,
                address: vacancy.companyShortInfo?.address,
                companyEmail: vacancy.companyShortInfo?.companyEmail,
                phoneNumber: vacancy.companyShortInfo?.phoneNumber
            }}
        });
}


export default async function DeleteVacancyReq(vacancyId: string) {
        
        const companyToken = Cookies.get('companyToken');
        
        if(!companyToken)
            throw new Error('Company token not found');
        
        const response = fetch(CompanySvcUrl + '/VacancyByCompany/DeleteVacancy?id=' + vacancyId,
            {
                method: 'POST',
                headers: {
                    'Authorization': 'Bearer ' + companyToken,
                    'Accept': '*/*',
                    'AcceptEncoding': 'gzip, deflate, br'
                },
                body: ""
            })
        
        const result = await response;
        console.log(result)
        if(!result.ok)
            throw new Error('Failed to delete vacancy');
        
        console.log('Vacancy deleted');
        alert('Vacancy deleted')
}


export async function UpdateVacancyReq(vacancy: VacancyToUpdateDto)  {
        
        const companyToken = Cookies.get('companyToken');
        
        if(!companyToken)
            throw new Error('Company token not found');
        
        const response = fetch(CompanySvcUrl + '/VacancyByCompany/UpdateVacancy',
            {
                method: 'POST',
                headers: {
                    'Authorization': 'Bearer ' + companyToken,
                    'Content-Type': 'application/json',
                    'Accept': '*/*',
                    'AcceptEncoding': 'gzip, deflate, br',
                },
                body: JSON.stringify(vacancy)
            })
        
        const result = await response;
        
        if(!result.ok)
            throw new Error('Failed to update vacancy');
        
        console.log('Vacancy updated');
        alert('Vacancy updated')
}