import {generateHeaderWithToken} from "@/lib/Helpers/requestHelper";
import ApplicationOnVacancy from "@/lib/Types/Vacancy/Messages/ApplicationOnVacancy";
import {AnswerOnApplicationToAddDto} from "@/lib/Types/Vacancy/Messages/AnswerOnApplicationToAddDto";

const companySvcUrl: string = "http://localhost:5240";

export async function getApplicationsOnVacancy(token: string, vacancyId: string) {
    const param = "?vacancyId=" + vacancyId;
    
    const response = await fetch(companySvcUrl + '/ApplicationOnVacancies/GetApplicationsOnVacancy' + param, { 
        method: 'GET',
        headers: generateHeaderWithToken(token)
    });
    
    if(!response.ok)
        throw new Error('Failed to get applications on vacancy');
    
    const json = await response.json();
    
    return json as ApplicationOnVacancy[];
}

export async function responseOnApplication(token: string, answer: AnswerOnApplicationToAddDto) {
    const response = await fetch(companySvcUrl + '/ApplicationOnVacancies/MakeFeedbackOnApplication', { 
        method: 'POST',
        headers: generateHeaderWithToken(token),
        body: JSON.stringify(answer)
    });
    
    if(!response.ok)
        console.log('Failed to make feedback on application')
    
    return response.ok;
}