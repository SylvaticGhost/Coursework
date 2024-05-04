'use client'

import {Component} from "react";
import MainHead from "@/Components/MainHead";

export default class ErrorWithCompanyAuthorizationComponent extends Component {
    render() {
        return (
            <div className="center-content">
                <MainHead text={"You're now unauthorized"} />
                <p className="my-2">You are not authorized to view this page.</p>
                <a className="purple-large-text" href='/CompanyHub/CompanyLogin'>Go to login</a>
            </div>
        );
    }
}