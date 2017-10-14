import { Injectable} from '@angular/core';

@Injectable()
export class MonthProvider {

    private _months: string[] = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

    getMonth(n) {
        return this._months[n]
    }

    monthList() {
        return this._months;
    }

}