'use client'
import React from 'react';
        
export default class BackButtonComponent extends React.Component { 
    render() {
        return (
            <button className="button default-purple-button" onClick={event => window.history.back()}>
                Back
            </button>
        )
    }
}