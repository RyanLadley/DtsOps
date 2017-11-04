import { Component, OnInit } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router'
import { ServerRequest } from '../../services/index';

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
    constructor(private _route: ActivatedRoute, private _router: Router, private _server: ServerRequest) {
        
    }

    ngOnInit() {
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
                this.selectFilter("Active");
            },
            error => { }
        )
    }

    /*vendors: any = [
        {
            "vendorId": 1,
            "name": "Flynn",
            "contractNumber": 9912298,
            "transfers": -3428.6402,
            "contractStart": "2016-08-23T12:30:00",
            "contractEnd": "2017-08-23T12:30:00",
            "pointOfContact": "Ramsey Abbott",
            "phoneNumber" : "719-555-2636",
            "email": "Gould@email.com",
            "website": "www.Gamble.com",
            "status": "Inactive"
        },
        {
            "vendorId": 1,
            "name": "Frederick",
            "contractNumber": 4251217,
            "transfers": -5529.7567,
            "contractStart": "2019-01-14T12:30:00",
            "contractEnd": "2017-01-23T12:30:00",
            "pointOfContact": null,
            "phoneNumber": null,
            "email": null,
            "website": null,
            "status": "Active"
        },
        {
            "vendorId": 1,
            "name": "Pacheco",
            "contractNumber": 3942082,
            "transfers": -3587.3178,
            "contractStart": "2017-04-28T12:30:00",
            "contractEnd": "2017-08-23T12:30:00",
            "pointOfContact": "Ford Conway",
            "email": "Lisa@email.com",
            "phoneNumber": "719-555-2636",
            "website": "www.Delacruz.com",
            "status": "Active"
        },
        {
            "vendorId": 1,
            "name": "Gill",
            "contractNumber": 9417021,
            "transfers": -7446.5209,
            "contractStart": "2017-08-23T12:30:00",
            "contractEnd": "2017-08-23T12:30:00",
            "pointOfContact": "Aimee Spence",
            "email": "Lenora@email.com",
            "phoneNumber": "719-555-2636",
            "website": "www.Woodard.com",
            "status": "Inactive"
        },
        {
            "vendorId": 1,
            "name": "Paul",
            "contractNumber": 283635,
            "transfers": -3528.4869,
            "contractStart": "2017-08-23T12:30:00",
            "contractEnd": "2017-08-23T12:30:00",
            "pointOfContact": "Terry Cochran",
            "email": "Jacobson@email.com",
            "phoneNumber": "719-555-2636",
            "website": "www.Bean.com",
            "status": "Active"
        },
        {
            "vendorId": 1,
            "name": "Holt",
            "contractNumber": 3890304,
            "transfers": -196.3628,
            "contractStart": "2017-08-23T12:30:00",
            "contractEnd": "2017-08-23T12:30:00",
            "phoneNumber": null,
            "pointOfContact": null,
            "email": null,
            "website": null,
            "status": "Active"
        }
    ]*/
}
