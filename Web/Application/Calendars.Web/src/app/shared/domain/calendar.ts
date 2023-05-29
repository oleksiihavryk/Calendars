import { Day } from "./day";

export class Calendar {
    // public get calendarType(): string {
    //     switch (this.type) {
    //         case 0: {
    //             return 'julian';
    //         }  
    //         case 1: {
    //             return 'gregorian'
    //         }
    //         default: {
    //             return 'unknown';
    //         }
    //     }
    // }
    // public set calendarType(value: string) {
    //     switch (value) {
    //         case 'julian': {
    //             this.type = 0;
    //             break;
    //         }  
    //         case 'gregorian': {
    //             this.type = 1;
    //             break;
    //         }
    //         default: {
    //             this.type = -1;
    //             break;
    //         }
    //     }
    // }

    constructor(
        public id: string,
        public userId: string,
        public name: string,
        public year: number,
        public type: number,
        public days: Day[]) { }
}
