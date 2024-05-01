import ApplicationOnVacancy from "@/lib/Types/Vacancy/Messages/ApplicationOnVacancy";

const backendUrl = 'http://localhost:5239';

export async function postApplication(application: ApplicationOnVacancy, token: string): Promise<void> {
    const response = await fetch(backendUrl + '/VacancyByUser/ResponseOnVacancy', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token,
            'connection': 'keep-alive',
            'Accept': 'application/json',
            'Access-Control-Allow-Origin': '*',
            'Accept-Encoding': 'gzip, deflate, br',
        },
        body: JSON.stringify(application)
    });
    
    if(!response.ok)
        throw new Error('Failed to post application');
    
    return;
}