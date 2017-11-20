import { Component, OnInit } from '@angular/core';
import { SiteLocation } from '../../models/index';
import { AuthService } from '../../services/index';
import { Router } from "@angular/router";

@Component({
  selector: 'sidebar',
  templateUrl: './sidebar.template.html'
})
export class SidebarComponent implements OnInit {

    icons: SiteLocation;
    expand: boolean;
    positionStyle: any;

    constructor(private _authService: AuthService, private _router: Router) {

    }

    ngOnInit() {
        this.expand = false;
        this.icons = new SiteLocation(this._router);
    }

    canDisplay(): boolean{
        return !(this._router.url == "/login")
    }
    signOut() {
        this._authService.logout();
    }

    getPermissions() {
        return this._authService.getPermissions();
    }

}
