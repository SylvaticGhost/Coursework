import {ShortResume} from "@/lib/Types/Vacancy/Messages/ShortResume";

export default class ApplicationOnVacancy {
    vacancyId: string;
    shortResume: ShortResume;
    
    constructor(vacancyId: string, shortResume: ShortResume) {
        this.vacancyId = vacancyId;
        this.shortResume = shortResume;
    }
}