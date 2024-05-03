'use client';

import {Component} from "react";
import "../Components/component.css";
import Link from "next/link";

type VacancyPreviewComponentProps = { 
    title: string,
    company: string,
    specialization: string,
    id: string
}

const url : string = 'http://localhost:3000'

export default class VacancyPreviewComponent extends Component<VacancyPreviewComponentProps> {
    render() {
        return (
            <Link href={url + '/Vacancy/' + this.props.id}>
                <div className="m-3 p-2 rounded-xl bg-blue-100 flex-column shrink min-width-content">
                    <p className="no-wrap font-semibold">{this.props.title}</p>
                    <p className="no-wrap">{this.props.company}</p>
                    <p className="no-wrap">{this.props.specialization}</p>
                </div>
            </Link>
        );
    }
}