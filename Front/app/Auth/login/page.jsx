//http://localhost:3000/Auth/login
'use client';

import {useState} from "react";
import {UserLogin} from "../../../lib/auth";
import {UserAuth} from "../../../lib/Types/UserAuth";
import {Route, Router, useNavigate} from "react-router-dom";
import Home from "../../page";
import {useRouter} from "next/router";
import Cookies from "js-cookie";

export default function Login() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
   

    async function login() {
        const userAuth = new UserAuth(email, password);

        try {
            const response = await UserLogin(userAuth);
            console.log(response);
            
            Cookies.set('token', response, { expires: 7, secure: true });
        } catch (error) {
            setError(error.message);
            console.log(error.message);
        }
    } 


    return (
        <div className="flex h-screen justify-center items-center">
            <div className="flex-column justify-center">
                <div
                    className="content-center flex-col flex justify-center items-center border-4 border-fuchsia-600 rounded-2xl p-8 w-min h-min">
                    <div className="py-3 flex-col">
                        <h2 className="font-semibold text-purple-500 flex justify-center items-center">Login</h2>
                        <br/>
                        <div className="py-2">
                            <input
                                type="email"
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                className="border-2 rounded-md"
                                placeholder={"Email or phone number"}
                            />
                        </div>
                    </div>
                    <div className="justify-center">

                        <div className="py-2">
                            <input
                                type="password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                className="border-2 rounded-md"
                                placeholder="Password"
                            />
                        </div>
                    </div>
                    <div className="py-3">
                        <button
                            className="my-2 flex justify-center items-center bg-fuchsia-500 p-2 rounded-xl text-amber-50 font-semibold
                            hover:bg-fuchsia-800 transition duration-200 active:scale-90"
                            onClick={login}>Login
                        </button>
                        <br/>
                        <a href="/Auth/register" className="text-purple-400">Create an account</a>

                    </div>
                </div>
                <div className="m-5  text-center">
                    {setError && ` ${error}`}
                </div>
            </div>
        </div>
    );
}

