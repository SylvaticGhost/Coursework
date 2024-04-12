export default class Vacancy {
    VacancyId: number;
    CreatedAt: Date;
    title: string;
    description?: string;
    salary?: string;
    specialization?: string;
    experience?: string;
    
    constructor(VacancyId: number, CreatedAt: Date, title: string, description?: string, salary?: string, specialization?: string, experience?: string) {
        this.VacancyId = VacancyId;
        this.CreatedAt = CreatedAt;
        this.title = title;
        this.description = description;
        this.salary = salary;
        this.specialization = specialization;
        this.experience = experience;
    }
}
