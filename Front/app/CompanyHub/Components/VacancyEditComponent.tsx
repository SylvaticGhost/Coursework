'use client'

import Vacancy from "@/lib/Types/Vacancy/Vacancy";
import '../CompanyHubStyle.css';
import DeleteVacancyReq from "@/lib/VacancyReqByCompany";
import {Component} from "react";

type VacancyEditComponentProps = { 
    vacancy: Vacancy
    refreshVacancies: () => void
}

export default class VacancyEditComponent extends Component<VacancyEditComponentProps> {
    render() {
        let {vacancy} = this.props;

        const deleteVacancy = async () => {
            console.log(vacancy)
            // @ts-ignore
            const id = vacancy.VacancyId;
            console.log(id)
            if (!id)
                throw new Error('Vacancy id not found(empty)');

            await DeleteVacancyReq(id);
            this.props.refreshVacancies();
        }

        return (
            <div className="m-1 py-2 px-5 rounded-xl bg-gray-200 border-4 border-gray-300 flex flex-row shrink">
                <a><span className="propInVacancyList font-bold mr-2">{vacancy.title}</span></a>
                <span className="propInVacancyList mr-2">{"Created: " + vacancy.createdAt}</span>
                <span
                    className='propInVacancyList mr-1'>{"Specialization: " + vacancy.specialization ?? "undefined"}</span>
                <button className="btnOnVacancy button-press">View</button>
                <button className="btnOnVacancy button-press">Edit</button>
                <button className="btnOnVacancy button-press" onClick={deleteVacancy}>Delete</button>
            </div>
        )
    }
}