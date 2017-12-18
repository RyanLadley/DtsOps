import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from "../../services/index";

@Component({
  selector: 'home',
  templateUrl: './home.template.html'
})
export class HomeComponent {
    searchString: string;

    constructor(private _router: Router, private _route: ActivatedRoute, private _authService: AuthService) {

    }

    gotoSearch(searchString: string) {
        this._router.navigate(['/search'], { queryParams: { search: searchString || '' } })
    }

    getName() {
        return this._authService.getFirstName();
    }

    getGreeting() {
        let date = new Date()
        let hour = date.getHours()

        if (hour < 12) {
            return "Morning"
        }
        else if (hour < 17) {
            return "Afternoon"
        }
        else {
            return "Evening"
        }
    }
}
