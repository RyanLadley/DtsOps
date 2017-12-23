import { Component, OnInit, HostListener, ElementRef } from '@angular/core';
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
    displayMobileMenu: boolean;

    constructor(private _authService: AuthService, private _router: Router, private _element: ElementRef) {

    }

    ngOnInit() {
        this.expand = false;
        this.icons = new SiteLocation(this._router);
    }

    canDisplay(): boolean {
        return !(this._router.url == "/login")
    }

    signOut() {
        this._authService.logout();
    }

    getPermissions() {
        var persmissions = this._authService.getPermissions();

        if (persmissions == null || persmissions == undefined) {
            this._authService.logout();
            return false;
        }
        else {
            return persmissions;
        }
    }

    //Mobile Stuff
    @HostListener('document:click', ['$event'])
    public documentClick(event: Event): void {
        if (!this._element.nativeElement.contains(event.target)) // or some similar check
            this.displayMobileMenu = false;
    }

    toggleMobileMenu() {
        this.displayMobileMenu = !this.displayMobileMenu
    }

}
