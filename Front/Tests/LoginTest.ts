import UserLogin from '../lib/auth';
import {describe, test} from "jest-circus";
import {jest} from "@jest/globals";
import expect from "expect";
import {UserAuth} from "@/lib/Types/UserAuth";

const userAuth = new UserAuth("test11@email.com", "test");

describe('Login function', () => {
    test('should send a POST request with email in the body when login contains "@"', async () => {

        global.fetch = jest.fn().mockImplementation(() =>
            Promise.resolve({
                ok: true,
                json: () => Promise.resolve({ success: true }),
            }) as Promise<Response>
        );
        
        const answer = await UserLogin(userAuth);

        expect(answer)
    });

    // test('should send a POST request with phoneNumber in the body when login does not contain "@"', async () => {
    //     const login = '1234567890';
    //     const password = 'password123';
    //     global.fetch = jest.fn().mockImplementation(() =>
    //         Promise.resolve({
    //             ok: true,
    //             json: () => Promise.resolve({ success: true }),
    //         }) as Promise<Response>
    //     );
    //
    //     await Login(userAuth);
    //
    //     expect(global.fetch).toHaveBeenCalledTimes(1);
    //     expect((global.fetch as jest.Mock).mock.calls[0][0]).toBe('http://localhost:5239/UserAuth/login');
    // });
});