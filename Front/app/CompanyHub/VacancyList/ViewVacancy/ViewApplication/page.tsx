'use client'

import Cookies from "js-cookie";
import ApplicationOnVacancy from "@/lib/Types/Vacancy/Messages/ApplicationOnVacancy";
import React, {useEffect, useState} from "react";
import ErrorWithCompanyAuthorizationComponent from "@/app/CompanyHub/Components/ErrorWithCompanyAuthorizationComponent";
import {getApplicationsOnVacancy} from "@/lib/Requests/VacancyApplication/VacancyApplicationByCompany";
import Vacancy from "@/lib/Types/Vacancy/Vacancy";
import MainHead from "@/Components/MainHead";
import BackButtonComponent from "@/Components/BackButtonComponent";
import {InternalErrorComponent} from "@/Components/InternalErrorComponent";
import {LoadingComponent} from "@/Components/LoadingComponent";
import ApplicationComponent
    from "@/app/CompanyHub/VacancyList/ViewVacancy/ViewApplication/Components/ApplicationComponent";
import App from "next/app";
import {DataForAnswer} from "@/lib/Types/Vacancy/Messages/DataForAnswer";

export default function ViewApplicationPage() {
    const [applications, setApplications] = useState<ApplicationOnVacancy[]>([]);
    
    const [loading, setLoading] = useState<boolean>(true);
    const [internalError, setInternalError] = useState<boolean>(false);
    
    const token = Cookies.get('companyToken');
    const vacancy: Vacancy = JSON.parse(localStorage.getItem('vacancy') ?? '{}');
    
    console.log(applications)
    
    // if(loading) 
    //     return <LoadingComponent/>
    
    
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
        setLoading(false);
    }, []);
    
    if(!vacancy)
        return (
            <div className="center-content">
                <MainHead text="Error with loading vacancy"/>
                <p className="my-2"><BackButtonComponent /></p>
            </div>
        )
    
    if(internalError){
        setLoading(false)
        return <InternalErrorComponent />;
    }
    
    
    return (
        <div className="center-content">
            <MainHead text="Applications list"/>
            {applications.length === 0 ? <p className="mt-10"><MainHead text="No Applications"/></p> : (
                applications.map((app, index) => (
                    checkApplication(app) ?
                        <ApplicationComponent
                            vacancyId={app.vacancyId}
                            userId={app.userId}
                            shortResume={app.shortResume}
                            dataForAnswer={{
                                vacancyId: app.vacancyId,
                                userId: app.userId,
                                userApplicationId: app.userApplicationId
                            } as DataForAnswer
                            }
                            key={index}/> : null
                ))
            )}
        </div>
    )
}

function checkApplication(app: ApplicationOnVacancy) { 
    if(!app.vacancyId)
        throw new Error('Vacancy Id is not provided at ApplicationComponent' + app.vacancyId);
    
    if(!app.userId)
        throw new Error('User Id is not provided at ApplicationComponent' + app.userId);
    
    if(!app.shortResume)
        throw new Error('Short Resume is not provided at ApplicationComponent' + app.shortResume);
    
    if(!app.userApplicationId)
        throw new Error('User Application Id is not provided at ApplicationComponent' + app.userApplicationId);
    
    const data: DataForAnswer = {
        vacancyId: app.vacancyId,
        userId: app.userId,
        userApplicationId: app.userApplicationId
    }
    
    if(!data)
        throw new Error('Grouped data is null')
    
    return true;
}