import {ShortResume} from "@/lib/Types/Vacancy/Messages/ShortResume";

export type ApplicationOnVacancyToAddDto = { 
    vacancyId: string;
    shortResume: ShortResume;
}