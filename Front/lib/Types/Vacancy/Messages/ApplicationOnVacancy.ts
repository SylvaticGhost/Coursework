import {ShortResume} from "@/lib/Types/Vacancy/Messages/ShortResume";
import {DataForAnswer} from "@/lib/Types/Vacancy/Messages/DataForAnswer";

export default class ApplicationOnVacancy {
    vacancyId: string;
    userId: string;
    userApplicationId?: string;
    shortResume: ShortResume;
    
    constructor(vacancyId: string,userId: string,shortResume: ShortResume) {
        this.vacancyId = vacancyId;
        this.shortResume = shortResume;
        this.userId = userId;
    }
    
    public getDataForAnswer(): DataForAnswer { 
        return {
            vacancyId: this.vacancyId,
            userId: this.userId,
            userApplicationId: this.userApplicationId
        }
    }
}