'use client';

import {Component} from "react";
import Cookies from "js-cookie";

export default class UserMessageBoxIconComponent extends Component {
    render() {
        const logged = Cookies.get('logged');
        
        if(logged !== 'true') {
            return null;
        }
        
        return (
            <button>
                <a className="font-bold p-2" href='/Profile/MessageBox'>M</a>
            </button>
        )
    }
}