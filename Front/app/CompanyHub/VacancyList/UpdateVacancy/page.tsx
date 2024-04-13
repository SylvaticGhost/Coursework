'use client'

import Vacancy from "@/lib/Types/Vacancy/Vacancy";
import MainHead from "@/Components/MainHead";
import ToCompanyHubComponent from "@/app/CompanyHub/Components/ToCompanyHubComponent";
import {useState} from "react";
import {UpdateVacancyReq} from "@/lib/VacancyReqByCompany";
import {VacancyToUpdateDto} from "@/lib/Types/Vacancy/VacancyToUpdateDto";

const url = 'http://localhost:3000';

export default function UpdateVacancyPage() {
    
    const vacancy: Vacancy = JSON.parse(localStorage.getItem('vacancy') ?? '{}');
    
    const [tittle, setTittle] = useState<string>(vacancy.title);
    const [description, setDescription] = useState<string>(vacancy.description ?? '');
    const [salary, setSalary] = useState<string>(vacancy.salary ?? '');
    const [specialization, setSpecialization] = useState<string>(vacancy.specialization ?? '');
    const [experience, setExperience] = useState<string>(vacancy.experience ?? '');
    
    const onUpdate = async () => { 
        
        const vacancyToUpdate : VacancyToUpdateDto = {
            // @ts-ignore
            vacancyId: vacancy.VacancyId,
            title: tittle,
            description: description,
            salary: salary,
            specialization: specialization,
            experience: experience
        }
        
        try {
            await UpdateVacancyReq(vacancyToUpdate);
        }
        catch(e) {
            console.log(e);
        }
    }
    
    return(
        <div>
            <div className="flex justify-center items-center h-screen">
                <div className="flex-column justify-items-center my-5">
                    <div>
                        <MainHead text="UPDATE VACANCY"/>
                    </div>
                    <div className="my-3">
                        <input className="border-2 rounded-md py-1 my-2"
                               value={tittle}
                               placeholder=" tittle"
                               onChange={e => setTittle(e.target.value)}/>
                    </div>
                    <div className="my-3">
                    <textarea className="border-2 rounded-md py-1 my-2"
                              value={description}
                              placeholder=" description"
                              onChange={e => setDescription(e.target.value)}/>
                    </div>
                    <div className="my-3">
                        <input className="border-2 rounded-md py-1 my-2"
                               value={salary}
                               placeholder=" salary"
                               onChange={e => setSalary(e.target.value)}/>
                    </div>
                    <div className="my-3">
                        <input className="border-2 rounded-md py-1 my-2"
                               value={specialization}
                               placeholder=" specialization"
                               onChange={e => setSpecialization(e.target.value)}/>
                    </div>
                    <div className="my-3">
                        <input className="border-2 rounded-md py-1 my-2"
                               value={experience}
                               placeholder=" expirience"
                               onChange={e => setExperience(e.target.value)}/>
                    </div>
                    <div className="mt-3 mb-5">
                        <button className="button-press rounded-2xl font-semibold p-3 text-lg bg-fuchsia-600 text-white shadow-xl
                            hover:bg-fuchsia-700"
                                onClick={onUpdate}>
                            Update
                        </button>
                    </div>
                    <div className="mb-7">
                        <ToCompanyHubComponent url={url}/>
                    </div>
                </div>
            </div>
        </div>
    );
}
