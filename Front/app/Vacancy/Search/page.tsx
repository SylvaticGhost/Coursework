'use client'

import React, {useEffect} from "react";
import MainHead from "@/Components/MainHead";
import Vacancy from "@/lib/Types/Vacancy/Vacancy";
import {searchVacancyByKeyword} from "@/lib/VacancySearch";
import VacancyPreviewComponent from "@/Components/VacancyPreviewComponent";

export default function SearchPage() { 
    
    let [searchTitle, setSearchTitle] = React.useState<string>('');
    
    let [foundedVacancies, setFoundedVacancies] = React.useState<Vacancy[] | null>(null);
    
    // useEffect(() => { 
    //     async function fetchFoundedVacancies() {
    //         const v = await searchVacancyByKeyword(searchTitle);
    //         setFoundedVacancies(v);
    //     }
    //     fetchFoundedVacancies();
    // });
    
    const findVacancies = async () => {
        const v = await searchVacancyByKeyword(searchTitle);
        console.log(v);
        setFoundedVacancies(v);
    }
    
    return ( 
        <div className="flex justify-center p-5">
            <div className="flex-column items-center">
                <MainHead text="Search"/>
                <input type="text" placeholder="search" className="border-2 border-gray-300 p-2 rounded-lg"
                onChange={event => {
                    setSearchTitle(event.target.value);
                }}/>
                <button className="bg-indigo-600 p-2 text-white rounded-lg ml-2 button-press" onClick={findVacancies}>Search</button>
            </div>
            <div className="flex-column items-center ml-6">
                <MainHead text="Founded vacancies"/>
                {foundedVacancies ? foundedVacancies.map((vacancy) => {
                    return (
                        <VacancyPreviewComponent title={vacancy.title} 
                                                 company={vacancy.companyShortInfo?.name ?? 'undef'} 
                                                 specialization={vacancy.specialization ?? ''} 
                                                 id={vacancy.vacancyId}/>
                    )
                }) : 'Empty'}
            </div>
        </div>
    )
}