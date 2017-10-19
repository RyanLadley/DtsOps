import { Component, OnInit } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router'
import { ServerRequest } from '../../services/index';
import { SearchResult } from '../../models/index';

@Component({
  selector: 'search',
  templateUrl: './search.template.html'
})
export class SearchComponent implements OnInit {


    routeSubscription: any;
    searchString: string;
    results: SearchResult[];
    filter: string;

    constructor(private _route: ActivatedRoute, private _router: Router) {
        
    }

    ngOnInit() {
        
        this.routeSubscription = this._route.queryParams.subscribe(queryParams => {
            this.searchString = queryParams['search'];
        });
        this.filter = "All";
        this.results = this.getSearchResults(this.searchString);
    }

    getSearchResults(search) {
        let tempResults: SearchResult[] = new Array();

        tempResults.push({id: 1, name: "1234523534", subName: "Grainger", type: "Ticket", expense: 12.25 } as SearchResult);
        tempResults.push({id: 4, name: "ASGDEGS", subName: "Kewit", type: "Invoice", expense: 12.25, description: "This is a description of a search result" } as SearchResult);
        tempResults.push({id: 5, name: "Blue Moon", subName: "1221f", type: "Vendor" } as SearchResult);


        return tempResults;
    }

    selectFilter(newFilter: string) {
        this.filter = newFilter;
    }

    getCompleteSubName(subName: string, type: string) {
        switch (type) {
            case "Ticket":
                return subName + " Ticket";

            case "Invoice":
                return subName + " Invoice";

            case "Vendor":
                return "Vendor Contract " + subName;
        }
    }

    getClassForType(type: string) {
        switch (type) {
            case "Ticket":
                return "ticket-result";

            case "Invoice":
                return "invoice-result";

            case "Vendor":
                return "vendor-result";
        }
    }

    gotoResultDetails(id: number, type: string) {
        switch (type) {
            case "Ticket":
                this._router.navigate(['/ticket', id]);
                break;

            case "Invoice":
                this._router.navigate(['/invoice', id]);
                break;

            case "Vendor":
                this._router.navigate(['/vendor', id]);
                break;
        }
    }

}
