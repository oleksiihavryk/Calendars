export class Event {
    constructor(
        public id: string,
        public dayId: string,
        public userId: string,
        public name: string,
        public hoursFrom: number,
        public hoursTo: number,
        public minutesFrom: number,
        public minutesTo: number,
        public description: string | null) { }
}
