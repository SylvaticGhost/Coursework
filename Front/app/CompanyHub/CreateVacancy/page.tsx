'use client'

import {useState} from "react";
import MainHead from "@/Components/MainHead";

export default function CreateVacancy() {

    const [tittle, setTittle] = useState<string>('');
    const [description, setDescription] = useState<string>('');
    const [salary, setSalary] = useState<string>('');
    const [specialization, setSpecialization] = useState<string>('');
    const [expirience, setExpirience] = useState<string>('');

    return(
        <div className="flex justify-center items-center h-screen">
            <div className="flex-column justify-items-center my-5">
                <div>
                    <MainHead text="CREATE VACANCY"/>
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
                           value={expirience}
                           placeholder=" expirience"
                           onChange={e => setExpirience(e.target.value)}/>
                </div>
                <div className="my-7">
                    <button className="button-press rounded-2xl font-semibold p-3 text-lg bg-fuchsia-600 text-white shadow-xl
                            hover:bg-fuchsia-700">
                        Create
                    </button>
                </div>
            </div>
        </div>
    )
}