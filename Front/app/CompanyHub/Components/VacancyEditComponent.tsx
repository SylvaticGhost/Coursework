import Vacancy from "@/lib/Types/Vacancy/Vacancy";

type VacancyEditComponentProps = { 
    vacancy: Vacancy
}

export default function VacancyEditComponent({vacancy}: VacancyEditComponentProps) {
    
    return (
        <div className="m-1 p-2 rounded-xl bg-gray-200 border-2 border-gray-300">
            <a>{vacancy.title}</a>
            <a>{"Created: " + vacancy.CreatedAt}</a>
            <a>{"Specialization:" + vacancy.specialization ?? "undefined"}</a>
            <button>View</button>
            <button>Edit</button>
            <button>Delete</button>
        </div>
    )
}