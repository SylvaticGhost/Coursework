import {Component} from "react";
import MainHead from "@/Components/MainHead";
import BackButtonComponent from "@/Components/BackButtonComponent";
import ToMainPageBtn from "@/Components/ToMainPageBtn";

export class InternalErrorComponent extends Component {
    render() {
        return (
            <div className="center-content">
                <MainHead text={'Internal error'}/>
                <div>
                    <img src="/internalError.jpeg" alt={''} style={{transform: "scale(0.5)"}}/>
                </div>
                <BackButtonComponent/>
                <div className="my-2"><ToMainPageBtn/></div>
            </div>
        );
    }
}