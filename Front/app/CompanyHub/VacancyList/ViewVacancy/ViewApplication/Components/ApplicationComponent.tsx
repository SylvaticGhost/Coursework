import {Component} from "react";
import ApplicationOnVacancy from "@/lib/Types/Vacancy/Messages/ApplicationOnVacancy";
import ShortResumeComponent from "@/Components/ShortResumeComponent";
import ResponseButton from "@/app/CompanyHub/VacancyList/ViewVacancy/ViewApplication/Components/ResponseButton";

type Props = {
    applications: ApplicationOnVacancy;
}

export default class ApplicationComponent extends Component<ApplicationOnVacancy> {
    render() { 
        return (
            <div className="m-2 rounded-xl bg-gray-200 border-4 border-gray-300">
                <ShortResumeComponent shortResume={this.props.shortResume} />
                <div className="flex flex-row">
                    <ResponseButton dataForAnswer={this.props.getDataForAnswer()} />
                    <button>
                        Reject
                    </button>
                </div>
            </div>
        )
    }
}