export function generateDefaultHeader() {
    return {
        'Content-Type': 'application/json',
        'Accept': '*/*',
        'AcceptEncoding': 'gzip, deflate, br',
        'Connection': 'keep-alive'
    }
}


export function generateHeaderWithToken(token: string) {
    return { 
        'Authorization': 'Bearer ' + token,
        'Content-Type': 'application/json',
        'Accept': '*/*',
        'AcceptEncoding': 'gzip, deflate, br',
        'Connection': 'keep-alive'
    } }