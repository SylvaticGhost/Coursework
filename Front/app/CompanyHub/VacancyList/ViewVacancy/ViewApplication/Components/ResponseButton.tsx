import {Component} from "react";
import {DataForAnswer} from "@/lib/Types/Vacancy/Messages/DataForAnswer";

type Props = {
    dataForAnswer: DataForAnswer;
}

export default class ResponseButton extends Component<Props> {
    render() {
        return (
            <button className="middle-button bg-blue-300 hover:bg-blue-400">
                Response
            </button>
        )
    }
}