import {Component} from "react";
import "../Components/component.css";

type VacancyPreviewComponentProps = { 
    title: string,
    company: string,
    specialization: string,
    id: string
}

export default class VacancyPreviewComponent extends Component<VacancyPreviewComponentProps> {
    render() {
        return (
            <div className="m-3 p-2 rounded-xl bg-blue-100 flex-column shrink min-width-content">
                <p className="no-wrap">{this.props.title}</p>
                <p className="no-wrap">{this.props.company}</p>
                <p className="no-wrap">{this.props.specialization}</p>
            </div>
        );
    }
}