import Vacancy from "@/lib/Types/Vacancy/Vacancy";

const url = 'http://localhost:5241'

export async function GetLatestVacancy(count : number) : Promise<Vacancy[] | null>{
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


export async function getVacancyById(id: string) {
    return fetch(`${url}/Vacancy/GetVacancy?id=${id}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Accept': '*/*',
            'AcceptEncoding': 'gzip, deflate, br',
            'Connection': 'keep-alive'
        },
    }).then(response => {
        if(response.ok) {
            return response.json();
        }
        else {
            console.log('Error: ' + response.statusText);
            return null;
        }
    });
}



export async function searchVacancyByKeyword(keywords: string) : Promise<Vacancy[] | null> {
    const response = await fetch(`${url}/Vacancy/SearchVacancyByKeyWords?keyWords=${keywords}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Accept': '*/*',
            'AcceptEncoding': 'gzip, deflate, br',
            'Connection': 'keep-alive'
        },
    });

    if(response.ok) {
        const json = await response.json();

        const mappedVacancies = json.map((vacancy: any) => new Vacancy(
            vacancy.vacancyId,
            new Date(vacancy.createdAt),
            vacancy.title,
            vacancy.description,
            vacancy.salary,
            vacancy.specialization,
            vacancy.experience
        ));

        return mappedVacancies;

    } else {
        console.log('Error: ' + response.statusText);
        return null;
    }
}