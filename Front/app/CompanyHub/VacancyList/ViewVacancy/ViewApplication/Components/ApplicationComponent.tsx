import {Component} from "react";
import ApplicationOnVacancy from "@/lib/Types/Vacancy/Messages/ApplicationOnVacancy";
import ShortResumeComponent from "@/Components/ShortResumeComponent";
import ResponseButton from "@/app/CompanyHub/VacancyList/ViewVacancy/ViewApplication/Components/ResponseButton";
import RejectButton from "@/app/CompanyHub/VacancyList/ViewVacancy/ViewApplication/Components/RejectButton";

type Props = {
    applications: ApplicationOnVacancy;
}

export default class ApplicationComponent extends Component<ApplicationOnVacancy> {
    render() { 
        
        if(!this.props.dataForAnswer)
            throw new Error('Data for answer is not provided at ApplicationComponent' + this.props.dataForAnswer);
        
        return (
            <div className="m-2 rounded-xl bg-gray-200 border-4 border-gray-300 p-2">
                <ShortResumeComponent shortResume={this.props.shortResume} />
                <div className="flex flex-row justify-between mt-3">
                    <ResponseButton dataForAnswer={this.props.dataForAnswer} />
                    <RejectButton dataForAnswer={this.props.dataForAnswer} />
                </div>
            </div>
        )
    }
}