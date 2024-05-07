import {Component} from "react";
import {ShortResume} from "@/lib/Types/Vacancy/Messages/ShortResume";
import "../app/CompanyHub/VacancyList/ViewVacancy/ViewApplication/ViewApplicationStyles.css"

type Props = {
    shortResume: ShortResume;
}

export default class ShortResumeComponent extends Component<Props> {
    render() {
        return (
            <div className="p-2 rounded-xl bg-amber-50 border-4 border-amber-100">
                <p className="field-in-resume">
                    <a className="font-semibold">Name</a>
                    <span className="ml-1">{this.props.shortResume.name}</span>
                </p>
                <p className="field-in-resume">
                    <a className="font-semibold">Contact</a>
                    <span className="ml-1">{this.props.shortResume.contact}</span>
                </p>
                <p className="field-in-resume">
                    <a className="font-semibold">Experience</a>
                    <span className="ml-1">{this.props.shortResume.experience}</span>
                </p>
                <p className="field-in-resume">
                    <a className="font-semibold">Education</a>
                    <span className="ml-1">{this.props.shortResume.education}</span>
                </p>
                <p className="field-in-resume">
                    <a className="font-semibold">Skills</a>
                    <span className="ml-1">{this.props.shortResume.skills}</span>
                </p>
                <p className="field-in-resume">
                    <a className="font-semibold">Additional Info</a>
                    <span className="ml-1">{this.props.shortResume.additionalInfo}</span>
                </p>
            </div>
        )
    }
}