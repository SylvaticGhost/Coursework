import {getVacancyById} from "@/lib/VacancySearch";

export async function generateMetadata ( {params: {slug}}: {params: {slug: string}}) {
    return {
        title: 'Create application on vacancy'
    }
}

export default async function CreateApplicationOnVacancy({params: {slug}}: {params: {slug: string}}) {
    const vacancy = await getVacancyById(slug);
}
