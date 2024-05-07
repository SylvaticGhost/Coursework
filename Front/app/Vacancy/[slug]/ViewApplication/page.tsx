'use client'

import Cookies from "js-cookie";
import React from "react";
import LogInComponent from "@/Components/LogInComponent";
import MainHead from "@/Components/MainHead";
import ToMainPageBtn from "@/Components/ToMainPageBtn";
import ApplicationOnVacancy from "@/lib/Types/Vacancy/Messages/ApplicationOnVacancy";
import {deleteApplication, getApplication} from "@/lib/Requests/VacancyApplication/VacancyApplicationByUser";
import {LoadingComponent} from "@/Components/LoadingComponent";
import ApplicationViewComponent from "@/app/Vacancy/Components/ApplicationViewComponent";
import BackButtonComponent from "@/Components/BackButtonComponent";

export default function ViewApplicationPage({params: {slug}}: {params: {slug: string}}) {
    const [loading, setLoading] = React.useState<boolean>(true);
    const [application, setApplication] = React.useState<ApplicationOnVacancy | null>(null);
    
    const token = Cookies.get('token');
    
    React.useEffect(() => {
        if (!token) {
            setLoading(false);
            return;
        }
        
        getApplication(slug, token).then(application => setApplication(application));
        
        setLoading(false);
    }, []);
    
    
    
    if (loading) {
        return <div>
            <LoadingComponent/>
        </div>
    }

    if (!token) {
        return <div className="center-content">
            <MainHead text="You're unauthorize"/>
            <p className="my-2">
                <LogInComponent />;
            </p>
            <ToMainPageBtn />
        </div>
    }
    
    if (!application) {
        return <div className="center-content">
            <MainHead text="Application not found"/>
            <p className="my-2">
                <BackButtonComponent />
            </p>
            <ToMainPageBtn />
        </div>
    }
    
    return (
        <div className="center-content">
            <ApplicationViewComponent application={application}/>
            <div className="flex flex-col m-2">
                <button className="btn btn-primary default-purple-button" onClick={() => window.history.back()}>Back</button>
                <p className="my-2">
                    <button className="default-red-button" onClick={event => {
                        if (confirm('Are you sure you want to delete this application?')) {
                             deleteApplication(application?.vacancyId, token).then(() => window.location.href = '/Vacancy/' + slug + '/ViewApplications');
                        }
                    }}>
                        Delete
                    </button>
                </p>
            </div>
        </div>
    )
}