'use client';

import Cookies from "js-cookie";

export function LogOut() {
    return (
        <div>
            <a onClick={logOut} className="text-purple-500">LogOut</a>
        </div>
    );
}

function logOut() {
    Cookies.remove('token');
    Cookies.remove('logged');
    window.location.href = 'http://localhost:3000/Auth/login';
}