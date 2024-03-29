export class UserRegister {
    public email: string;
    public phoneNumber: string;
    public password: string;
    public firstName: string;
    public lastName: string;
    public birthDate: string;
    
    constructor(email: string, phoneNumber: string, password: string, firstName: string, lastName:string, birthDate: string) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.birthDate = birthDate;
        this.password = password;
    }
    
    public toString(): string { 
        return this.email + '\n' + 
            this.phoneNumber + '\n ' + 
            this.password + '\n ' + 
            this.firstName + '\n' + 
            this.lastName + '\n ' + 
            this.birthDate
    }
    
}