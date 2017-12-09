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
    displayedResults: SearchResult[];
    filter: string;
    searching: boolean;
    constructor(private _server: ServerRequest, private _route: ActivatedRoute, private _router: Router) {
        
    }

    ngOnInit() {
        
        this.routeSubscription = this._route.queryParams.subscribe(queryParams => {
            this.searchString = queryParams['search'];
            this.getSearchResults(this.searchString);
        });
        this.filter = "All";
    }

    getSearchResults(search) {
        this.searching = true;
        this._server.get('api/search', null, { searchString: search }).subscribe(
            response => { this.results = response; this.selectFilter(this.filter); this.searching = false; },
            error => { }
        )
    }
    

    getCompleteSubName(subName: string, type: string) {
        switch (type) {
            case "Ticket":
                return subName + " Ticket";

            case "Invoice":
                return subName + " Invoice";

            case "Vendor":
                return "Vendor Contract " + subName;

            case "Material":
                return "Material in units of " + subName;
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

            case "Material":
                return "material-result";
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

            case "Material":
                this._router.navigate(['/material', id]);
                break;
        }
    }

    selectFilter(filter: string) {
        this.filter = filter;

        if (filter == "All") {
            this.displayedResults = this.results;
        }
        else {
            this.displayedResults = []
            for (var i = 0; i < this.results.length; i++) {
                if (this.results[i].type == filter) {
                    this.displayedResults.push(this.results[i]);
                }
            }
        }
    }

    gotoSearch(searchString: string) {
        this._router.navigate(['/search'], { queryParams: { search: searchString || '' } })
    }
}
