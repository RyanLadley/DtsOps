import { Injectable, EventEmitter } from '@angular/core';

@Injectable()
export class ObjectSort {

    sortByString(objects, property, ascending) {
        objects.sort(function (a, b) {
            if (!ascending) {
                var temp = a
                a = b
                b = temp
            }

            var stringA = a[property].toLowerCase(), stringB = b[property].toLowerCase();

            if (stringA < stringB) {
                return -1;
            }
            if (stringA > stringB) {
                return 1;
            }
            return 0
        })

        return objects;
    }

}