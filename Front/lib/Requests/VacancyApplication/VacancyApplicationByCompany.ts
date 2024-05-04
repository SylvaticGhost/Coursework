import {generateHeaderWithToken} from "@/lib/Helpers/requestHelper";
import ApplicationOnVacancy from "@/lib/Types/Vacancy/Messages/ApplicationOnVacancy";

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