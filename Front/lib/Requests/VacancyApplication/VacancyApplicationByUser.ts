import ApplicationOnVacancy from "@/lib/Types/Vacancy/Messages/ApplicationOnVacancy";

const backendUrl = 'http://localhost:5239';

export async function postApplication(application: ApplicationOnVacancy, token: string): Promise<void> {
    const response = await fetch(backendUrl + '/VacancyByUser/ResponseOnVacancy', {
        method: 'POST',
        headers: getHeaders(token),
        body: JSON.stringify(application)
    });
    
    if(!response.ok)
        throw new Error('Failed to post application');
    
    return;
}

export async function checkIfUserApplied(vacancyId: string, token: string): Promise<boolean> {
    const param = "?vacancyId=" + vacancyId;
    const response = await fetch(backendUrl + '/VacancyByUser/CheckIfUserApplied' + param, {
        method: 'GET',
        headers: getHeaders(token)
    });
    
    if(!response.ok)
        throw new Error('Failed to check if user applied');
    
    const json = await response.json();
    console.log(json);
    return  json as boolean;
    
}

export async function getApplication(vacancyId: string, token: string): Promise<ApplicationOnVacancy> {
    const param = "?vacancyId=" + vacancyId;
    const response = await fetch(backendUrl + '/VacancyByUser/GetMyApplicationOnVacancy' + param, {
        method: 'GET',
        headers: getHeaders(token)
    });
    
    if(!response.ok)
        throw new Error('Failed to get application');
    
    const json = await response.json();
    return json as ApplicationOnVacancy;
}

export async function deleteApplication(vacancyId: string, token: string): Promise<void> { 
    const param = "?vacancyId=" + vacancyId;
    const response = await fetch(backendUrl + '/VacancyByUser/DeleteApplicationByVacancyId' + param, {
        method: 'POST',
        headers: getHeaders(token)
    });
    
    if(!response.ok)
        throw new Error('Failed to delete application');
    
    return;
}

function getHeaders(token: string) {
    return {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token,
        'connection': 'keep-alive',
        'Accept': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Accept-Encoding': 'gzip, deflate, br',
    };
}