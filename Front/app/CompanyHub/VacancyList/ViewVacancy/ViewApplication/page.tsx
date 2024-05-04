'use client'

import Cookies from "js-cookie";
import ApplicationOnVacancy from "@/lib/Types/Vacancy/Messages/ApplicationOnVacancy";
import {useEffect, useState} from "react";
import ErrorWithCompanyAuthorizationComponent from "@/app/CompanyHub/Components/ErrorWithCompanyAuthorizationComponent";
import {getApplicationsOnVacancy} from "@/lib/Requests/VacancyApplication/VacancyApplicationByCompany";
import Vacancy from "@/lib/Types/Vacancy/Vacancy";
import MainHead from "@/Components/MainHead";
import BackButtonComponent from "@/Components/BackButtonComponent";
import {InternalErrorComponent} from "@/Components/InternalErrorComponent";
import {LoadingComponent} from "@/Components/LoadingComponent";

export default function ViewApplicationPage() {
    const [applications, setApplications] = useState<ApplicationOnVacancy[]>([]);
    
    const [loading, setLoading] = useState<boolean>(true);
    const [internalError, setInternalError] = useState<boolean>(false);
    
    const token = Cookies.get('companyToken');
    const vacancy: Vacancy = JSON.parse(localStorage.getItem('vacancy') ?? '{}');
    
    
    if(loading) 
        <LoadingComponent/>
    
    
    if(!token)
        return <ErrorWithCompanyAuthorizationComponent />;

    useEffect(() => {
        // @ts-ignore
        getApplicationsOnVacancy(token ?? '', vacancy.VacancyId).then(apps => {
            setApplications(apps);
        }).catch(e => {
            setInternalError(true);
            console.log(e)
        }).finally(() => {setLoading(false)});
    }, []);
    
    if(!vacancy)
        return (
            <div className="center-content">
                <MainHead text="Error with loading vacancy"/>
                <p className="my-2"><BackButtonComponent /></p>
            </div>
        )
    
    if(internalError){
        return <InternalErrorComponent />;
    }
    
    return (
        <div>
            
        </div>
    )
}