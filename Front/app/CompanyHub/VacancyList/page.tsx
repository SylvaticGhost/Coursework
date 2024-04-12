'use client'

import React, { useEffect, useState } from 'react';
import {GetCompanyVacancies} from "@/lib/VacancyReqByCompany";
import Vacancy from "@/lib/Types/Vacancy/Vacancy";
import VacancyEditComponent from "@/app/CompanyHub/Components/VacancyEditComponent";

const url = 'http://localhost:3000'

export default function VacancyListPage() {
    const [vacancies, setVacancies] = useState<Vacancy[]>([]);

    const fetchVacancies = async () => {
        const fetchedVacancies = await GetCompanyVacancies();
        setVacancies(fetchedVacancies);
    };

    useEffect(() => {
        fetchVacancies();
    }, []);
    
    //TODO: calibrate the button
    return (
        <div className="flex justify-center m-8">
            <div className="flex-column justify-items-center">
                <div className="m-4 p-1 min-width-content">
                {vacancies.map(vacancy => {
                    // @ts-ignore trouble with case sensitivity
                    if (!vacancy.VacancyId) {
                        console.error('Missing vacancyId for vacancy:', vacancy);
                        return null; // Don't render this vacancy.
                    }
                    // @ts-ignore
                    return <VacancyEditComponent key={vacancy.VacancyId} vacancy={vacancy} refreshVacancies={fetchVacancies}/>;
                })}   
                </div>
                <div className="flex justify-items-center max-width-content">
                    <a className="font-semibold text-purple-500 text-xl text-center" href={url + '/CompanyHub/Company'}>TO COMPANY PAGE</a>

                </div>
            </div>
        </div>
    )
}