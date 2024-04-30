export default class UnauthorizedException extends Error {
    public readonly statusCode = 401;
    
    constructor(public readonly subject: string) {
        super('Unauthorized');
    }
}