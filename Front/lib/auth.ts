import {UserAuth} from "@/lib/Types/UserAuth";
import {UserRegister} from "@/lib/Types/UserRegister";

const url : string = 'http://localhost:5239/UserAuth'

export async function UserLogin(userAuth: UserAuth) { 
    
    let typeLogin : string;
    if(userAuth.email.includes("@"))
        typeLogin  = "email";
    else
        typeLogin = "phoneNumber";
    
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
    
    if(response.ok)
        return response.json();
    
    throw new Error('Failed to login');
}


export async function UserRegistration(userRegister: UserRegister) { 
    
}