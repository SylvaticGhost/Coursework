'use client'

const url = 'http://localhost:3000'
export default function CompanyPage() { 
    
    return(
        <div className="flex justify-center">
            <div className="my-8 flex-row">
                <h1 className="text-xl text-purple-600 font-semibold pb-6">Company Page</h1>
                <button className="button-press rounded-2xl font-semibold p-3 text-lg bg-fuchsia-600 text-white shadow-xl
                        hover:bg-fuchsia-700"
                        onClick={e => window.location.href=url + '/CompanyHub/CreateVacancy'}>
                    Create vacancy
                </button>
            </div>
        </div>
    )
}