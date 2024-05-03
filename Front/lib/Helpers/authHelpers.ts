import {UserRegister} from "@/lib/Types/UserRegister";

export function ValidRegistrationForm(userRegister: UserRegister): string {
    if(userRegister.email === '') {
        return 'Email is required'
    }
    
    if(userRegister.phoneNumber === '') {
        return 'Phone number is required'
    }
    
    if(userRegister.password === '') {
        return 'Password is required'
    }
    
    if(userRegister.firstName === '') {
        return 'Name is required'
    }
    
    if(userRegister.birthDate === '') {
        return 'Birth date is required'
    }
    
    if(userRegister.password.length < 8) {
        return 'Password must be at least 8 characters long'
    }
    
    if(userRegister.phoneNumber.length < 8) {
        return 'Phone number must be at least 8 characters long'
    }
    
    return ''
}

export function validateEmail(email: string): boolean{
    const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}