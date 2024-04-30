import React, {Component} from "react";
import MainHead from "@/Components/MainHead";
import LogInComponent from "@/Components/LogInComponent";
import ToMainPageBtn from "@/Components/ToMainPageBtn";

export default class NotLoggedComponent extends Component {
    render() {
        return <div className="center-content">
            <MainHead text="Not logged in"/>
            <p className="my-2">
                <LogInComponent/>
            </p>
            <ToMainPageBtn/>
        </div>;
    }
}