//TODO: upgrade regex for email
//TODO: bug with writing a year from keyboard
'use client'

import {useState} from "react";
import {UserRegister} from "@/lib/Types/UserRegister";
import {ValidRegistrationForm} from "@/lib/Helpers/authHelpers";
import {UserRegistration} from "@/lib/auth";



export default function Register() {
    
    const [email, setEmail] = useState('')
    const [phone, setPhone] = useState('')
    const [password, setPassword] = useState('')
    const [confirmPassword, setConfirmPassword] = useState('')
    const [firstName, setFirstName] = useState('')
    const [lastname, setLastname] = useState('')
    const [birthDate, setBirthDate] = useState(new Date())
    const [terms, setTerms] = useState(false)
    const [error, setError] = useState('')
    const [showPassword, setShowPassword] = useState(false)
    
    function CreateAccount(event: any) {
        event.preventDefault();
        if(!terms) {
            setError('You must accept terms')
            return
        }
        
        if(password !== confirmPassword) {
            setError('Passwords do not match')
            return
        }
        
        const user : UserRegister = {
            email: email,
            phoneNumber: phone,
            password: password,
            lastName: firstName,
            firstName: lastname,
            birthDate: birthDate.toISOString().split('T')[0]
        }
        
        const valid = ValidRegistrationForm(user)
        
        if(valid !== '') {
            setError(valid)
            return
        }
        
        
            const response = UserRegistration(user)
            console.log(response)
           
            window.location.href = 'http://localhost:3000/Auth/CreateProfile'
        
    }
    
    return (
        <div className="flex h-screen justify-center items-center m-4">
            <div className="flex-row">
                <div className="register__container p-4 rounded-2xl border-2 border-fuchsia-600 m-2">
                    <h2 className="font-semibold text-purple-500 flex justify-center items-center">Create account</h2>
                    <div className="register__container__form">
                        <form>
                            <div className="register__container__form__input my-2">
                                <input type="email" 
                                       placeholder=" Email" 
                                       className="border-2 rounded-md"
                                       value={email}
                                       onChange={e => {
                                           setEmail(e.target.value);
                                       }}/>
                            </div>
                            <div className="register__container__form__input  my-2">
                                <input type="text" 
                                       placeholder=" Phone Number" 
                                       className="border-2 rounded-md"
                                       value={phone}
                                       onChange={e => {
                                           setPhone(e.target.value.replace(/[^0-9+]/g, ''));
                                       }}/>
                            </div>
                            <div className="register__container__form__input  my-2">
                                <input type={showPassword ? "text" : "password"} 
                                       placeholder="Password"
                                       className="border-2 rounded-md"
                                       value={password}
                                       onChange={e => setPassword(e.target.value)}/>
                            </div>
                            <div className="register__container__form__input  my-2">
                                <input type={showPassword ? "text" : "password"} placeholder="Confirm Password"
                                       className="border-2 rounded-md"
                                       value={confirmPassword}
                                       onChange={e => setConfirmPassword(e.target.value)}/>
                            </div>
                            <div className="register__container__form__input  my-2">
                                <input type="text" placeholder="Name" className="border-2 rounded-md"
                                       value={firstName}
                                       onChange={e => {
                                           setFirstName(e.target.value.replace(/[^a-zA-Zа-яёА-ЯЁіІєЄїЇґҐ\s]/g, ''));
                                       }}/>
                            </div>
                            <div className="register__container__form__input  my-2">
                                <input type="text" placeholder="Lastname" className="border-2 rounded-md"
                                       value={lastname}
                                       onChange={e => {
                                           setLastname(e.target.value.replace(/[^a-zA-Zа-яёА-ЯЁіІєЄїЇґҐ\s]/g, ''));
                                       }}/>
                            </div>
                            <div className="register__container__form__input  my-2">
                                <input type="date" placeholder="Birth Date" className="border-2 rounded-md"
                                       value={birthDate.toISOString().split('T')[0]}
                                       onChange={e => setBirthDate(new Date(e.target.value))}/>
                            </div>
                            <div className="flex justify-center py-1">
                                <button type="button" onClick={() => setShowPassword(!showPassword)}
                                className="text-purple-500 hover:text-purple-700">
                                    {showPassword ? "Hide Password" : "Show Password"}
                                </button>
                            </div>
                            <div className="flex justify-center">
                                <input type="checkbox" 
                                       className="m-2 text-purple-500 hover:text-purple-700"
                                       value={terms.toString()}
                                       onChange={e => setTerms(e.target.checked)}/>
                                <a>I accept the terms</a>
                            </div>
                            <br/>
                            <div className="register__container__form__submit my-2 flex justify-center items-center">
                                <button type="submit"
                                        className="bg-fuchsia-700 rounded-md p-2 text-amber-50 font-semibold
                                        hover:bg-fuchsia-800 transition duration-200 active:scale-90"
                                        onClick={CreateAccount}>
                                    Create
                                </button>
                            </div>

                        </form>
                    </div>
                </div>
                <div className="pt-3 flex justify-center items-center">
                    {error && <div className="text-red-500">{error}</div>}
                </div>
            </div>

        </div>
    )
}