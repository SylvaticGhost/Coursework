import React, {Component} from "react";
import {LogOut} from "@/Components/LogOut";
import ToMainPageBtn from "@/Components/ToMainPageBtn";

export default class FailedToLoadProfileComponent extends Component { 
    render() {
        return (
            <div className="center-content">
                <h3>Failed to load profile</h3>
                <LogOut/>
                <ToMainPageBtn/>
            </div>
        )
    }
}