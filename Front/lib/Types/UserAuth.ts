export class UserAuth {
    email: string;
    phoneNumber: string;
    password: string;

    constructor(login: string, password: string) {
        if (login.includes("@")) {
            this.email = login;
            this.phoneNumber = "";
        } else {
            this.phoneNumber = login;
            this.email = "";
        }

        this.password = password;
    }

    // Method to validate email
    private validateEmail(): boolean {
        const re = /\S+@\S+\.\S+/;
        return re.test(this.email);
    }

    // Method to validate phone number
    private validatePhoneNumber(): boolean {
        const re = /^\d{10}$/;
        return re.test(this.phoneNumber);
    }
    
    
    login() {
        if(this.email !== "")
            return this.email;
        else
            return this.phoneNumber;
    }
    
    
    validate() {
        if(this.email === "" && this.phoneNumber === "")
            return false;
        if(this.email !== "" && this.validateEmail())
            return true;
        if(this.phoneNumber !== "" && this.validatePhoneNumber())
            return true;
        return false;
    }
}