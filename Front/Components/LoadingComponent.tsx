import React from "react";
import MainHead from "@/Components/MainHead";

export class LoadingComponent extends React.Component {
    render() {
        return <div className="center-content">
            <MainHead text="Loading..."/>
        </div>
    }
}