import { Component, OnInit } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router'
import { ServerRequest, AuthService } from '../../services/index';
import { SearchResult } from '../../models/index';

@Component({
  selector: 'profile',
  templateUrl: './profile.template.html'
})
export class ProfileComponent implements OnInit {

    users: any;
    newUser: any;
    editUser: any;
    currentUser: any;
    newBug: any;
    bugs: any;
    displayedFilter: string;
    displayedBugs: any[];
    userEditError: string;
    newUserError: string;
    tab: string;
    bugErrorMessage: string;

    constructor(private _server: ServerRequest, private _authService: AuthService) {
        
    }

    ngOnInit() {
        this.tab = 'User';
        this.newUser = { permissions: 1 };
        this.editUser = { permissions: 1 };
        this.currentUser = {};
        this.getCurrentUser();
        
    }

    selectTab(tab) {
        this.newUserError = null
        this.userEditError = null;
        this.tab = tab;
        this.newBug = {};
        if (tab == "Admin") {
            this.getUserListing();
        }
        if (tab == "Bug") {
            this.displayedFilter = "All";
            this.getBugs();
        }
    }

    registerNewUser() {
        if (this.newUser.passphrase && this.newUser.passphrase != this.newUser.confirmPassword) {
            this.newUserError = "Passwords do not match";
            return;
        }

        this._server.post('api/user/register', this.newUser).subscribe(
            response => {
                this.newUserError = "User Created Successfully";
                this.newUser = { permissions: 1 };
            },
            error => { this.newUserError = error }
        )
    }

    editUserInformation(userToEdit: any) {
        if (userToEdit.passphrase && userToEdit.passphrase != userToEdit.confirmPassword) {
            this.userEditError = "Passwords do not match";
            return;
        }

        this._server.post('api/user/edit', userToEdit).subscribe(
            response => {
                this.userEditError = "User Edited Successfully";
                this.currentUser = response;
            },
            error => { this.userEditError = error }
        )
    }

    getCurrentUser() {
        this._server.get('api/user').subscribe(
            response => {
                this.currentUser = response;
            },
            error => {}
        )
    }

    getUserListing() {
        this._server.get('api/user/listing').subscribe(
            response => {
                this.users = response;
                this.editUser = this.users[0];
            },
            error => { }
        )
    }

    getPermissions() {
        return this._authService.getPermissions();
    }

    setDisplayedFilter(filter: string) {
        this.displayedFilter = filter;
        if (this.displayedFilter == 'All') {
            this.displayedBugs = this.bugs;
        }
        else {
            this.displayedBugs = []
            for (var i = 0; i < this.bugs.length; i++) {
                if (this.bugs[i].severity == filter) {
                    this.displayedBugs.push(this.bugs[i]);
                }
            }
        }
    }

    getBugs() {
        this._server.get('api/bug').subscribe(
            response => {
                this.bugs = response;
                this.setDisplayedFilter(this.displayedFilter);
            },
            error => { }
        )
    }

    addNewBug() {
        this._server.post('api/bug', this.newBug).subscribe(
            response => {
                this.bugs = response;
                this.setDisplayedFilter(this.displayedFilter);
                this.newBug = {}
            },
            error => { this.bugErrorMessage = error; }
        )
    }
}
