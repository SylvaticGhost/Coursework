import {getVacancyById} from "@/lib/VacancySearch";
import React from "react";
import MainHead from "@/Components/MainHead";
import ApplyOnVacancyBtnComponent from "@/app/Vacancy/Components/ApplyOnVacancyBtnComponent";

export async function generateMetadata ( {params: {slug}}: {params: {slug: string}}) {
    const vacancy = await getVacancyById(slug);
    return {
        title: vacancy.title,
    }
}

export default async function ReviewPage({params: {slug}}: {params: {slug: string}}) {
    const vacancy = await getVacancyById(slug);
    
    const salary = vacancy.salary ? vacancy.salary : 'Not specified';
    const specialization = vacancy.specialization ? vacancy.specialization : 'Not specified';
    const experience = vacancy.experience ? vacancy.experience : 'Not specified';
    
    return (
        <div className="center-content">
            <MainHead text={vacancy.title}/>
            <div className="blue-container">
                <h3 className="font-semibold">Description</h3>
                <p>{vacancy.description}</p>
            </div>
            <div className="blue-container">
                <h3 className="font-semibold">Specialization</h3>
                <p>{specialization}</p>
            </div>
            <div className="blue-container">
                <h3 className="font-semibold">Experience</h3>
                <p>{experience}</p>
            </div>
            <div className="blue-container">
                <h3 className="font-semibold">Salary</h3>
                <p>{salary}</p>
            </div>
            <ApplyOnVacancyBtnComponent vacancyId={vacancy.vacancyId}/>
        </div>
    )
}