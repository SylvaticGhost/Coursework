'use client'

import MainHead from "@/Components/MainHead";
import Vacancy from "@/lib/Types/Vacancy/Vacancy";
import ToCompanyHubComponent from "@/app/CompanyHub/Components/ToCompanyHubComponent";
import React from "react";
import ToVacancyListComponent from "../../Components/ToVacancyListComponent";

const url = 'http://localhost:3000';

export default function ViewVacancyPage() {
    
    const vacancyString = localStorage.getItem('vacancy');
    if (!vacancyString) {
        throw new Error('Vacancy not found');
    }
    const vacancy: Vacancy = JSON.parse(vacancyString);
    
    return (
        <div className="center-content">
            <MainHead text={"View Vacancy"} />
            <table className="bordered-table mt-2">
                <tbody>
                <tr>
                    <th>Title</th>
                    <td>{vacancy.title}</td>
                </tr>
                <tr>
                    <th>Description</th>
                    <td>{vacancy.description}</td>
                </tr>
                <tr>
                    <th>Salary</th>
                    <td>{vacancy.salary}</td>
                </tr>
                <tr>
                    <th>Specialization</th>
                    <td>{vacancy.specialization}</td>
                </tr>
                <tr>
                    <th>Experience</th>
                    <td>{vacancy.experience}</td>
                </tr>
                </tbody>
            </table>
            <p className="my-3">
                <ToVacancyListComponent url={url}/>
            </p>
            <ToCompanyHubComponent url={url}/>
        </div>
    )
}