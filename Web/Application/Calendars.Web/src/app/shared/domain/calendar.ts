import { Day } from "./day";

export class Calendar {
    constructor(
        public id: string,
        public userId: string,
        public name: string,
        public year: number,
        public type: number,
        public days: Day[]) { }
}
