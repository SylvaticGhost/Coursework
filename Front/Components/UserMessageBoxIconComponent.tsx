'use client';

import {Component} from "react";
import Cookies from "js-cookie";
import Image from "next/image";

export default class UserMessageBoxIconComponent extends Component {
    render() {
        const logged = Cookies.get('logged');
        
        if(logged !== 'true') {
            return null;
        }
        
        return (
            <button className="mr-1"
            onClick={e => window.location.href = '/Profile/MessageBox'}>
                <img src="/msg.png" width="30" height="30" alt='M'/>
            </button>
        )
    }
}