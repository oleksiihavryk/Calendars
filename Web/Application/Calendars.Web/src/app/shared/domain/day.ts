import { Event } from "./event";

export interface IArgbColor {
    a: number,
    r: number,
    g: number,
    b: number
}
export class Day {
    public get backgroundArgbColor(): IArgbColor {
        const num = this.backgroundArgbColorInteger;
        const a = (num << 0) >>> 24,
              r = (num << 8) >>> 24,
              g = (num << 16) >>> 24, 
              b = (num << 24) >>> 24;
        return { a:a, r:r, g:g, b:b };
    }
    public get textArgbColor(): IArgbColor {
        const num = this.textArgbColorInteger;
        const a = (num << 0) >>> 24,
              r = (num << 8) >>> 24,
              g = (num << 16) >>> 24, 
              b = (num << 24) >>> 24;
        return { a:a, r:r, g:g, b:b };
    }
    public set backgroundArgbColor(argb: IArgbColor) {
        const {a,r,g,b} = argb;
        this.backgroundArgbColorInteger = (a << 24) + (r << 16) + (g << 8) + (b << 0);
    }
    public set textArgbColor(argb: IArgbColor) {
        const {a,r,g,b} = argb;
        this.textArgbColorInteger = (a << 24) + (r << 16) + (g << 8) + (b << 0);
    }

    constructor(
        public id: string,
        public calendarId: string,
        public dayNumber: number,
        public backgroundArgbColorInteger: number,
        public textArgbColorInteger: number,
        public events: Event[]) { }
}
