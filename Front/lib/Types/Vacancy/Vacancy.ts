import {CompanyShortInfo} from "@/lib/Types/Companies/CompanyShortInfo";

export default class Vacancy {
    vacancyId: string;
    createdAt: Date;
    updatedAt?: Date;
    title: string;
    description?: string;
    salary?: string;
    specialization?: string;
    experience?: string;
    companyShortInfo? : CompanyShortInfo
    
    
    constructor(VacancyId: string, CreatedAt: Date, title: string, description?: string, salary?: string, specialization?: string, experience?: string) {
        this.vacancyId = VacancyId;
        this.createdAt = CreatedAt;
        this.title = title;
        this.description = description;
        this.salary = salary;
        this.specialization = specialization;
        this.experience = experience;
    }
}

//fix returning after delete application