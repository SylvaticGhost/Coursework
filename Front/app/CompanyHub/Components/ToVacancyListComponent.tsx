import React, {Component} from "react";

type ToCompanyHubComponentProps = {
    url: string
}

export default class ToCompanyHubComponent extends Component<ToCompanyHubComponentProps> {
    render() {
        return (
            <a className="font-semibold text-purple-500 text-xl text-center" href={this.props.url + '/CompanyHub/VacancyList'}>TO VACANCY LIST</a>
        )}; }