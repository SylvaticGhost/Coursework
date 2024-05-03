'use client'

import {getVacancyById} from "@/lib/VacancySearch";
import React from "react";
import MainHead from "@/Components/MainHead";
import Vacancy from "@/lib/Types/Vacancy/Vacancy";
import Cookies from "js-cookie";
import {checkIfUserApplied, postApplication} from "@/lib/VacancyApplication";
import {ShortResume} from "@/lib/Types/Vacancy/Messages/ShortResume";
import ApplicationOnVacancy from "@/lib/Types/Vacancy/Messages/ApplicationOnVacancy";
import ToMainPageBtn from "@/Components/ToMainPageBtn";
import LogInComponent from "@/Components/LogInComponent";
import {LoadingComponent} from "@/Components/LoadingComponent";

export default function CreateApplicationOnVacancy({params: {slug}}: {params: {slug: string}}) {
    
    const token = Cookies.get('token');
    
    const [vacancy, setVacancy] = React.useState<Vacancy | null>(null);
    
    const [name, setName] = React.useState<string>('');
    const [contact, setContact] = React.useState<string>('');
    const [experience, setExperience] = React.useState<string>('');
    const [education, setEducation] = React.useState<string>('');
    const [skills, setSkills] = React.useState<string>('');
    const [additional, setAdditional] = React.useState<string>('');
    
    const [error, setError] = React.useState<string | null>(null);
    
    const [loading, setLoading] = React.useState<boolean>(true);
    const [userApplied, setUserApplied] = React.useState<boolean>(false);
    
    React.useEffect(() => {
        getVacancyById(slug).then(vacancy => setVacancy(vacancy));
        
        if (!token) {
            setLoading(false);
            return;
        }
        
        checkIfUserApplied(slug, token).then(result => setUserApplied(result));
        console.log('checkIfUserApplied' + userApplied);
        setLoading(false);
    }, []);
    
    const postApplicationOnVacancy = async () => { 
        if (vacancy) {
            const shortResume : ShortResume = { 
                name: name,
                contact: contact,
                experience: experience,
                education: education,
                skills: skills,
                additionalInfo: additional,
            }
            
            const application : ApplicationOnVacancy = {
                vacancyId: vacancy.vacancyId,
                shortResume: shortResume
            }
            
            try {
                if (!token) {
                    setError('Token not found');
                    return;
                }

                await postApplication(application, token);
                setUserApplied(true);
                
            } catch (e) {
                setError('Failed to post application');
            }
        }
    }
    
    if (loading)
        return <div className="center-content"><LoadingComponent/></div>
    
    if (error) 
        return (
            <div className="center-content">
                <p className="mb-2">{error}</p>
                <ToMainPageBtn/>
            </div>
        )
    
    if (!token)
        return (
            <div className="center-content">
                <p>You must be signed in for applying</p>
                <p className="my-2">
                    <LogInComponent/>
                </p>
                <ToMainPageBtn/>
            </div>
        )
    
    if (userApplied) {
        return (
            <div className="center-content">
                <p className="font-semibold mb-2 text-lg">You have already applied to this vacancy</p>
                <ToMainPageBtn/>
                <div className="mt-2">
                    <button className="button default-purple-button"
                    onClick={event => window.location.href='/Vacancy/' + slug + '/ViewApplication'}>
                        View
                    </button>
                </div>
            </div>
        )
    }
    
    return ( 
        <div className="center-content">
            <MainHead text="CREATE APPLICATION"/>
            <form className="flex flex-col items-center my-4 w-3/4 mx-15">
                <input type="text" 
                       placeholder=" Name" 
                       value={name} 
                       className="default-text-input"
                       onChange={(e) => setName(e.target.value)}/>
                <textarea placeholder=" Contact" 
                          value={contact} 
                          className="text-area"
                          onChange={(e) => setContact(e.target.value)}/>
                <textarea placeholder=" Experience" 
                          value={experience} 
                          className="medium-text-area"
                          onChange={(e) => setExperience(e.target.value)}/>
                <textarea placeholder=" Education"
                          value={education} 
                          className="medium-text-area"
                          onChange={(e) => setEducation(e.target.value)}/>
                <textarea placeholder=" Skills" 
                          value={skills} 
                          className="medium-text-area"
                          onChange={(e) => setSkills(e.target.value)}/>
                <textarea placeholder=" Additional" 
                          value={additional} 
                          className="large-text-area"
                          onChange={(e) => setAdditional(e.target.value)}/>
            </form>
            <button className="button default-purple-button"
            onClick={postApplicationOnVacancy}>
                Submit
            </button>
        </div>
    )
}
