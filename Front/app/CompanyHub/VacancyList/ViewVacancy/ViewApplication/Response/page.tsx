'use client'

import { InternalErrorComponent } from "@/Components/InternalErrorComponent";
import { use, useEffect, useState } from "react";
import Cookies from "js-cookie";
import ErrorWithCompanyAuthorizationComponent from "@/app/CompanyHub/Components/ErrorWithCompanyAuthorizationComponent";
import { AnswerOnApplicationToAddDto } from "@/lib/Types/Vacancy/Messages/AnswerOnApplicationToAddDto";
import { responseOnApplication } from "@/lib/Requests/VacancyApplication/VacancyApplicationByCompany";
import MainHead from "@/Components/MainHead";
import BackButtonComponent from "@/Components/BackButtonComponent";
import Vacancy from "@/lib/Types/Vacancy/Vacancy";

export default function ResponsePage() {
    const [text, setText] = useState('');

    const [error, setError] = useState(false);
    const [success, setSuccess] = useState(false);

    const [companyToken, setCompanyToken] = useState('');
    const [userApplicationId, setUserApplicationId] = useState('');
    const [vacancy, setVacancy] = useState<Vacancy | null>(null);

    useEffect(() => {
        if (typeof window !== 'undefined' && window !== null) {
            const token = Cookies.get('companyToken');
            setCompanyToken(token ?? '');
            if ('localStorage' in window) {
                const vacancy = JSON.parse(localStorage.getItem('vacancy') as string);
                setVacancy(vacancy);
                const userApplicationId = localStorage.getItem('applicationId');
                setUserApplicationId(userApplicationId ?? '');
            }
        }
    }, []);

    if (!userApplicationId || error)
        return <InternalErrorComponent />

    if (!companyToken)
        return <ErrorWithCompanyAuthorizationComponent />

    async function send() {
        console.log('send');
        const answer: AnswerOnApplicationToAddDto = {
            vacancyId: vacancyId,
            userApplicationId: userApplicationId as string,
            text: text
        }

        const result = await responseOnApplication(companyToken ?? ' ', answer);

        if (!result)
            setError(true);
        else
            setSuccess(true);
    }

    if (success)
        return (
            <div className="center-content">
                <MainHead text="Response created" />
                <p className="my-2">
                    <BackButtonComponent />
                </p>
            </div>
        )

    return (
        <div className="center-content">
            <MainHead text="Response" />
            <form method="post" className="mt-5">
                <div className="mb-3">
                    <textarea id="message-2"
                        className="p-1 rounded-sm w-full border-2 border-gray-300"
                        name="message" rows={6}
                        placeholder="Message"
                        value={text}
                        onChange={(e) => setText(e.target.value)}
                    />
                </div>
                <div className="flex flex-row justify-center mt-2"> 
                    <button className="default-purple-button"
                        type="submit"
                        onClick={e => send()}>
                        Send
                    </button>
                </div>
            </form>
        </div>
    )
}