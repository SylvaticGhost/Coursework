//http://localhost:3000/Auth/login
'use client';

import {useState} from "react";
import {UserLogin} from "../../../lib/auth";
import {UserAuth} from "../../../lib/Types/UserAuth";


export default function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  
    const login = async () => { 
        const userAuth = new UserAuth(email, password);
        
        try {
            const response = await UserLogin(userAuth);
            console.log(response);
        } catch (error) {
            setError(error.message);
            console.log(error);
        }
    };
    

  return (
      <div className="flex h-screen justify-center items-center">
      <div className="m-5 absolute top-0 left-0 right-0 text-center">
          {setError && `Error: ${error}`}
      </div>
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
                      className="my-2 flex justify-center items-center bg-fuchsia-500 p-2 rounded-xl text-amber-50 font-semibold" 
                      onClick={login}>Login</button>
                  <br/>
                  <a href="/Auth/register" className="text-purple-400">Create an account</a>

              </div>
          </div>
      </div>
  );
}