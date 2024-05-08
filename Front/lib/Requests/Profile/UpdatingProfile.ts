import { generateHeaderWithToken } from "@/lib/Helpers/requestHelper"
import { NameDto } from "@/lib/Types/NameDto"

const backUrl = 'http://localhost:5239/UserAuthUpdatingData'

export async function changePassword(newPassword: string, token: string) : Promise<boolean>{
    const param = '?newPassword=' + newPassword;

    const response = await fetch(backUrl + '/UdpatePassword', {
        'method' : 'POST',
        'headers' : generateHeaderWithToken(token)
    })

    return response.ok;
}


export async function updateName(nameDto: NameDto, token: string) : Promise<boolean>{
    const response = await fetch(backUrl + '/UdpateName', {
        'method' : 'POST',
        'headers' : generateHeaderWithToken(token),
        body: JSON.stringify(nameDto)
    });

    return response.ok;
}


export async function updatePhoneNumber(newPhoneNumber: string, token: string) : Promise<boolean> {
    const param = '?newPhoneNumber=' + newPhoneNumber;

    const response = await fetch(backUrl + '/UpdatePhoneNumber', {
        'method': 'POST',
        'headers' : generateHeaderWithToken(token)
    })

    return response.ok;
}