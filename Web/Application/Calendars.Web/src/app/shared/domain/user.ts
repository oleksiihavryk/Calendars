export class User {
    constructor(
        public userId: string, 
        public name: string,
        public email: string | undefined = undefined,
        public oldPassword: string | undefined = undefined,
        public newPassword: string | undefined = undefined) {}
}
