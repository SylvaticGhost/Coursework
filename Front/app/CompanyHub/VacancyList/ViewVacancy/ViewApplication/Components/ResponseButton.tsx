'use client'

import {Component} from "react";
import {DataForAnswer} from "@/lib/Types/Vacancy/Messages/DataForAnswer";
import "../ViewApplicationStyles.css"

type Props = {
    dataForAnswer: DataForAnswer;
}

export default class ResponseButton extends Component<Props> {
    render() {
        if(!this.props.dataForAnswer)
            throw new Error('Data for answer is not provided' + this.props.dataForAnswer);
        
        return (
            <button className="bg-blue-300 hover:bg-blue-400 answering-button middle-button"
            onClick={e => {
                localStorage.setItem('applicationId', this.props.dataForAnswer.userApplicationId ?? ' ')
                window.location.href = '/CompanyHub/VacancyList/ViewVacancy/ViewApplication/Response'
            }}>
                Response
            </button>
        )
    }
}