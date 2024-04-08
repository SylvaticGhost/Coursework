export default class CompanyToAddDto {
    name: string;
    email: string;
    address?: string;
    phoneNumber: string;
    webSite?: string;
    logo?: Blob | undefined;
    description: string;
    industry?: string;
    
    constructor(name: string, email: string, phoneNumber: string, description: string, address?: string, webSite?: string, logo?: Blob, industry?: string) {
        this.name = name;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.description = description;
        this.address = address;
        this.webSite = webSite;
        this.logo = logo;
        this.industry = industry;
    }
}