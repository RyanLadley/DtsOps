import { Router } from "@angular/router";

export class SiteLocation {
    home: boolean;
    overview: boolean;
    entry: boolean;
    reports: boolean;
    vendors: boolean;
    profile: boolean;

    constructor(private _router: Router) {
        this._router.events.subscribe((event) => { this.setLocation()});
    }

    setLocation() {
        let path = this._router.url;
        this.resetLocation();

        if (path === "/") {
            this.home = true;
        }
        else if (/\/overview*/.test(path) || /\/account*/.test(path) || /\/invoice*/.test(path) || /\/ticket*/.test(path) || /\/material*/.test(path)) {
            this.overview = true;
        }
        else if (/\/entry*/.test(path)) {
            this.entry = true;
        }
        else if (/\/report*/.test(path)) {
            this.reports = true;
        }
        else if (/\/vendor*/.test(path)) {
            this.vendors = true;
        }
        else if (/\/profile*/.test(path)) {
            this.profile = true;
        };
    }

    resetLocation() {
        this.home = false;
        this.overview = false;
        this.entry = false;
        this.reports = false;
        this.vendors = false;
        this.profile = false;
    }
}