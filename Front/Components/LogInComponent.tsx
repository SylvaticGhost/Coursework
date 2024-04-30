import React, {Component} from "react";
import MainHead from "@/Components/MainHead";

const url = "http://localhost:3000";

export default class LogInComponent extends Component {
    render() {
        return (
            <a href={url + '/Auth/login'} className="text-xl text-purple-500 font-semibold">SIGN IN</a>
        )
    }
}