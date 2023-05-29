export interface IArgbColor {
    a: number,
    r: number,
    g: number,
    b: number
}
export class Day {
    public get argbColor(): IArgbColor {
        const num = this.argbColorInteger;
        const a = (num << 0) >>> 24,
              r = (num << 8) >>> 24,
              g = (num << 16) >>> 24, 
              b = (num << 24) >>> 24;
        return {
            a: a,
            r: r,
            g: g,
            b: b
        };
    }
    public set argbColor(value: IArgbColor) {
        const {a,r,g,b} = value;
        this.argbColorInteger = (a << 24) + (r << 16) + (g << 8) + (b << 0);
    }

    constructor(
        public id: string,
        public calendarId: string,
        public dayNumber: number,
        public argbColorInteger: number,
        public events: Event[]) { }
}
