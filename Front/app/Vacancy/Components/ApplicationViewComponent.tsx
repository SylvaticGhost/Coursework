import React from "react";
import ApplicationOnVacancy from "@/lib/Types/Vacancy/Messages/ApplicationOnVacancy";
import MainHead from "@/Components/MainHead";

type Props = { 
    application: ApplicationOnVacancy 
};

export default class ApplicationViewComponent extends React.Component<Props> {
    render() {
        return <div>
            <MainHead text="Application"/>
            <div className="my-1">
                <h3 className="font-semibold">Name</h3>
                <p>{this.props.application.shortResume.name}</p>
            </div>
            <div className="my-1">
                <h3 className="font-semibold">Contact</h3>
                <p>{this.props.application.shortResume.contact}</p>
            </div >
            <div className="my-1">
                <h3 className="font-semibold">Skills</h3>
                <p>{this.props.application.shortResume.skills}</p>
            </div>
            <div className="my-1">
                <h3 className="font-semibold">Experience</h3>
                <p>{this.props.application.shortResume.experience}</p>
            </div>
            <div className="my-1">
                <h3 className="font-semibold">Education</h3>
                <p>{this.props.application.shortResume.education}</p>
            </div>
            <div className="my-1">
                <h3 className="font-semibold">Additional</h3>
                <p>{this.props.application.shortResume.additionalInfo}</p>
            </div>
        </div>
    }
}