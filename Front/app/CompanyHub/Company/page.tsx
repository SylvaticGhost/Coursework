'use client'

const url = 'http://localhost:3000'
export default function CompanyPage() { 
    
    return(
        <div className="center-content">
            <div className="my-8 flex-column">
                <h1 className="text-xl text-purple-600 font-semibold pb-6">Company Page</h1>
                <div className="">
                    <button className="button-press rounded-2xl font-semibold p-3 text-lg bg-fuchsia-600 text-white shadow-xl
                        hover:bg-fuchsia-700"
                            onClick={e => window.location.href=url + '/CompanyHub/CreateVacancy'}>
                        Create vacancy
                    </button>
                    <br/>
                    <button className="button-press rounded-2xl font-semibold p-3 my-5 text-lg bg-fuchsia-600 text-white shadow-xl
                        hover:bg-fuchsia-700"
                            onClick={e => window.location.href=url + '/CompanyHub/VacancyList'}>
                        Vacancies</button>
                </div>
            </div>
        </div>
    )
}