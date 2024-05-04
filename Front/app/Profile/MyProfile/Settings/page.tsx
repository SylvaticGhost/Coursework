import React from "react";
import MainHead from "@/Components/MainHead";

export default function SettingsPage() { 
    return ( 
        <div className="center-content"> 
            <MainHead text="SETTINGS"/>
            <div className="m-2 gray-container">
                <a className="font-semibold"> Change password</a>
            </div>
            <div className="m-2 gray-container">
                <a className="font-semibold"> Change email</a>
            </div>
            <div className="m-2 gray-container">
                <a className="font-semibold"> Change phone number</a>
            </div>
            <h3 className="m-2 font-bold text-xl text-red-600">DANGEROUS AREA</h3>
            <div className="m-2 gray-container flex flex-row">
                <a className="font-semibold"> Delete account and data</a>
                <button className="default-red-button">Delete</button>
            </div>
        </div>
    )
}