'use client'

import {Component} from "react";

type Props = {
    vacancyId: string
}

export default class ApplyOnVacancyBtnComponent extends Component<Props> {
    render() {
        return (
            <button className="button default-purple-button"
            onClick={event => window.location.href='/Vacancy/' + this.props.vacancyId + '/CreateApplicationOnVacancy'}>
                Apply
            </button>
        )
    }
}