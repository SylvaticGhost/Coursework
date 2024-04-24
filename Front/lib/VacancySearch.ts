import Vacancy from "@/lib/Types/Vacancy/Vacancy";

const url = 'http://localhost:5241'

export default async function GetLatestVacancy(count : number) : Promise<Vacancy[] | null>{
    const response = await fetch(`${url}/Vacancy/GetLatestVacancies?count=${count}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Accept': '*/*',
            'AcceptEncoding': 'gzip, deflate, br',
            'Connection': 'keep-alive'
        },
    });
    
    let data;
    if(response.ok) {
        data = await response.json();
        return data.map((vacancy: any) => new Vacancy(
            vacancy.vacancyId,
            new Date(vacancy.createdAt),
            vacancy.title,
            vacancy.description,
            vacancy.salary,
            vacancy.specialization,
            vacancy.experience
        ));
    }
    else {
        console.log('Error: ' + response.statusText);
       return null;
    }
}