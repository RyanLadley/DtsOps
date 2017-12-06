import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'home',
  templateUrl: './home.template.html'
})
export class HomeComponent {

    constructor(private  _router: Router, private _route: ActivatedRoute) {

    }

    gotoSearch(searchString: string) {
        this._router.navigate(['/search'], { queryParams: { search: searchString || '' } })
    }
}
