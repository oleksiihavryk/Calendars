export class User {
    constructor(
        public id: string, 
        public name: string,
        public email: string | undefined = undefined,
        public password: string | undefined = undefined) {}
}
