import {UserAuth} from "@/lib/Types/UserAuth";
import {UserRegister} from "@/lib/Types/UserRegister";
import Cookies from "js-cookie";

const url : string = 'http://localhost:5239/UserAuth'

export async function UserLogin(userAuth: UserAuth) { 
    console.log('UserLogin')
    let typeLogin : string;
    if(userAuth.email.includes("@"))
        typeLogin  = "email";
    else
        typeLogin = "phoneNumber";
    
    console.log(userAuth)
    
    const response = await fetch(url + '/login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': '*/*',
            'AcceptEncoding': 'gzip, deflate, br',
        },
        body: JSON.stringify({
            [typeLogin]: userAuth.login(),
            password: userAuth.password,
        }),
    });
    
    const token: string = await response.text()
    console.log('token: ' + token)
    if(response.ok) {
        Cookies.set('token', token, {expires: 7, secure: true})
        Cookies.set('logged', 'true')
        console.log('Logged in')
        console.log('token from cookie: '+ Cookies.get('token'))
        return token;
    }
    console.log("Response is not ok")
    //error handling
    const contentType = response.headers.get('content-type');
    if(contentType && contentType.includes('application/json')) {
        const data = await response.json();
        throw new Error(data.message);
    } else {
        const data = await response.text();
        throw new Error(data);
    }
    
}


export async function UserRegistration(userRegister: UserRegister) { 
    
    console.log(userRegister.toString())
    
    const response = await fetch(url + '/register', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Accept': '*/*',
            'AcceptEncoding': 'gzip, deflate, br',
        },
        body: JSON.stringify({
            firstName: userRegister.firstName,
            lastName: userRegister.lastName,
            email: userRegister.email,
            phoneNumber: userRegister.phoneNumber,
            birthDate: userRegister.birthDate,
            password: userRegister.password,
        }),
    });
    console.log(response)
    
    if(response.ok)
        response.json();
    else
        console.log("Response is not ok in registration endpoint")
    
    console.log(response.text())
    
    //Perform first login
    try {
        const user = new UserAuth(userRegister.email, userRegister.password)
        return await UserLogin(user)
    }
    catch (e) {
        console.log('error: ' + e)
    }
}