import { Component, OnInit } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router'
import { ServerRequest, ArraySort } from '../../services/index';

@Component({
  selector: 'vendor-overview',
  templateUrl: './vendor-overview.template.html'
})
export class VendorOverviewComponent implements OnInit {


    routeSubscription: any;
    page: string;
    displayedVendors: any[]
    vendors: any[];
    filter: string;


    currentSort: string;
    sortAscending: boolean;


    constructor(private _route: ActivatedRoute, private _router: Router, private _server: ServerRequest, private _sorter : ArraySort) {
        
    }

    ngOnInit() {
        this.currentSort = "name";
        this.filter = "Active";
        this.getVendors();
    }

    selectFilter(filter: string) {
        this.filter = filter;

        if (filter == "All") {
            this.displayedVendors = this.vendors;
        }
        else {
            this.displayedVendors = []
            for (var i = 0; i < this.vendors.length; i++) {
                if (this.vendors[i].status == filter) {
                    this.displayedVendors.push(this.vendors[i]);
                }
            }
        }
    }

    getSortIcon(field) {
        if (field == this.currentSort) {
            if (this.sortAscending) {
                return "fa-caret-up";
            }
            else {
                return "fa-caret-down";
            }
        }
    }

    getStatusIcon(status: string) {
        if (status == "Active") {
            return "fa-check green"
        }
        if (status == "Inactive") {
            return "fa-exclamation red"
        }
    }

    getVendors() {
        this._server.get('api/vendor/overview').subscribe(
            response => {
                this.vendors = response;
                this.sortBy(this.currentSort)
            },
            error => { }
        )
    }

    sortBy(field) {
        //Set the asscention direction. If this is a new feild, it is defaulted to ascending, if the user clicked the same field, we toggle the direction
        var direction: boolean;
        if (field == this.currentSort) {
            direction = !this.sortAscending
        }
        else {
            direction = true;
        }

        this.currentSort = field;
        this.sortAscending = direction;

        this._sorter.sort(this.vendors, field, direction);
        this.selectFilter(this.filter);
    }
}
