'use client';

import Cookies from "js-cookie";
import MainHead from "@/Components/MainHead";
import Vacancy from "@/lib/Types/Vacancy/Vacancy";
import {useEffect, useState} from "react";
import VacancyPreviewComponent from "@/Components/VacancyPreviewComponent";
import {GetLatestVacancy} from "@/lib/VacancySearch";

export default function Home() {
    console.log("Home");
    const token = Cookies.get('token');
    const logged = Cookies.get('logged') === 'true';
    console.log(token);
    
    const [latestVacancies, setLatestVacancies] = useState<Vacancy[] | null>(null);

    useEffect(() => {
        async function fetchLatestVacancies() {
            const v = await GetLatestVacancy(3);
            setLatestVacancies(v);
        }
        fetchLatestVacancies();
    }, [latestVacancies]);
    
  return (
    <main>
        <div className="vacancies-block">
            <MainHead text="Popular vacancy"/>
            <div className="vacancy-container">
                {latestVacancies ? latestVacancies.map((vacancy) => {
                    return (
                        <VacancyPreviewComponent title={vacancy.title}
                                                 company={vacancy.companyShortInfo?.name ?? 'undef'}
                                                 specialization={vacancy.specialization ?? ''} id={vacancy.vacancyId}/>
                    )
                }) : null}
            </div>
        </div>

        {
            logged ? (
                    <div className="vacancies-block">
                        <MainHead text="Recommended vacancy"/>
                        <div className="vacancy-container">
                            {latestVacancies ? latestVacancies.map((vacancy) => {
                                return (
                                    <VacancyPreviewComponent title={vacancy.title}
                                                             company={vacancy.companyShortInfo?.name ?? 'undef'}
                                                             specialization={vacancy.specialization ?? ''}
                                                             id={vacancy.vacancyId}/>
                                )
                            }) : null}
                        </div>
                    </div>
                ) :
                null
        }

        <div className="vacancies-block">
            <MainHead text="New vacancy"/>
            <div className="vacancy-container">
                {latestVacancies ? latestVacancies.map((vacancy) => {
                    return (
                        <VacancyPreviewComponent title={vacancy.title} 
                                                 company={vacancy.companyShortInfo?.name ?? 'undef'} 
                                                 specialization={vacancy.specialization ?? ''} 
                                                 id={vacancy.vacancyId}/>
                    )
                }) : null}
            </div>
        </div> 

    </main>
  );
}
