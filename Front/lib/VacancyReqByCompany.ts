import {VacancyToAddDto} from "@/lib/Types/Vacancy/VacancyToAddDto";
import Cookies from "js-cookie";

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