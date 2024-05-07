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


    get dataForAnswer(): DataForAnswer {
        const data: DataForAnswer = {
            vacancyId: this.vacancyId,
            userId: this.userId,
            userApplicationId: this.userApplicationId
        }

        if(!data.vacancyId || !data.userId || !data.userApplicationId)
            throw new Error('Data for answer is not provided' + JSON.stringify(data));
        
        console.log(JSON.stringify(data));
        
        return data;
    }
}