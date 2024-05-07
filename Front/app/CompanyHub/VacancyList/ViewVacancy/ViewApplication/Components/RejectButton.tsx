'use client'

import {DataForAnswer} from "@/lib/Types/Vacancy/Messages/DataForAnswer";
import {Component} from "react";
import "../ViewApplicationStyles.css"

type Props = {
    dataForAnswer: DataForAnswer;
}

export default class RejectButton extends Component<Props> {
    render() {
        return (
            <button className="answering-button bg-red-300 hover:bg-red-400 middle-button">
                Reject
            </button>
        )
    }
}