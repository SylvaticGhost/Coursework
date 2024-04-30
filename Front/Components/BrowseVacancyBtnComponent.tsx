'use client';

import {Component} from "react";

export default class BrowseVacancyBtnComponent extends Component { 
    render() { 
        return(
            <div className="m-3 flex justify-center">
                <button className="button-press bg-indigo-600 p-3 text-2xl rounded-2xl text-amber-50 font-semibold"
                onClick={event => {
                    window.location.href = '/Vacancy/Search';
                }}>
                    BROWSE
                </button>
            </div>
        )
    }
}