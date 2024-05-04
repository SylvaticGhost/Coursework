import { generateHeaderWithToken } from "./Helpers/requestHelper";

const backUrl: string = "http://localhost:5239";

export async function GetUserMessageBox(token: string) {
    const response = await fetch(  backUrl + "/UserMessages/GetMyMessages", { 
        method: 'GET', 
        headers: generateHeaderWithToken(token)
    });     

    const data = await response.json();
}