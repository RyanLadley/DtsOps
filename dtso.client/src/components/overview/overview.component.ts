import { Component, OnInit } from '@angular/core';
import { ServerRequest } from '../../services/index';
import { AccountForm } from "../../models/index";

@Component({
    selector: 'overview',
    templateUrl: './overview.template.html'
})
export class OverviewComponent implements OnInit {
    collapse: boolean;
    accounts: any;
    editAccounts: any;
    edit: boolean;


    constructor(private _server: ServerRequest) {

    }

    ngOnInit() {
        this.collapse = false;
        this.accounts = this.getAccounts();
        this.edit = false;
        this.toggleCollapse();
    }

    toggleCollapse() {
        if (this.accounts) {
            this.collapse = !this.collapse;

            for (var i = 0; i < this.accounts.length; i++) {
                this.accounts[i].hideChildren = this.collapse;

                for (var j = 0; j < this.accounts[i].childAccounts.length; j++) {
                    this.accounts[i].childAccounts[j].hideChildren = this.collapse;
                }
            }
        }
    }


    getAccounts() {
        this._server.get('api/account/overview').subscribe(
            response => { this.accounts = response },
            error => { }
        )
    }

    toggleAccountEdit() {
        this.edit = !this.edit
        
        if (this.edit)
            this.editAccounts = JSON.parse(JSON.stringify(this.accounts));
        else
            this.accounts = JSON.parse(JSON.stringify(this.editAccounts));
    }

    addSubAccount(index) {
        if (!this.accounts[index].newChildAccounts)
            this.accounts[index].newChildAccounts = []

        let newAccount = new AccountForm();
        newAccount.accountNumber = this.accounts[index].accountNumber;

        newAccount.subNo = this.accounts[index].childAccounts.length + this.accounts[index].newChildAccounts.length + 1

        this.accounts[index].newChildAccounts.push(newAccount);
    }

    removeNewSubAccount(index, newSubIndex) {

        //Decrement Sub numbers of all added after to keep numbering consitant
        for (var i = newSubIndex + 1; i < this.accounts[index].newChildAccounts.length; i++){
            this.accounts[index].newChildAccounts[i].subNo -= 1;
        }
        this.accounts[index].newChildAccounts.splice(newSubIndex, 1);
    }

    addShredAccount(index, subIndex) {
        if (!this.accounts[index].childAccounts[subIndex].newChildAccounts)
            this.accounts[index].childAccounts[subIndex].newChildAccounts = []

        let newAccount = new AccountForm();
        newAccount.accountNumber = this.accounts[index].childAccounts[subIndex].accountNumber;
        newAccount.subNo = this.accounts[index].childAccounts[subIndex].subNo;
        
        newAccount.shredNo = this.accounts[index].childAccounts[subIndex].childAccounts.length + this.accounts[index].childAccounts[subIndex].newChildAccounts.length + 1

        this.accounts[index].childAccounts[subIndex].newChildAccounts.push(newAccount);
    }

    removeNewShredAccount(index, subIndex, newShredIndex) {

        //Decrement Sub numbers of all added after to keep numbering consitant
        for (var i = newShredIndex + 1; i < this.accounts[index].childAccounts[subIndex].newChildAccounts.length; i++) {
            this.accounts[index].childAccounts[subIndex].newChildAccounts[i].shredNo-= 1;
        }
        this.accounts[index].childAccounts[subIndex].newChildAccounts.splice(newShredIndex, 1);
    }

    submitAccountUpdates() {
        
        var accountForms : AccountForm[] = []
        for (var i = 0; i < this.accounts.length; i++) {
            accountForms.push(AccountForm.MapFromAccount(this.accounts[i]));

            for (var j = 0; j < this.accounts[i].childAccounts.length; j++) {
                accountForms[i].childAccounts.push(AccountForm.MapFromAccount(this.accounts[i].childAccounts[j]));

                for (var k = 0; k < this.accounts[i].childAccounts[j].length; k++) {
                    accountForms[i].childAccounts[j].childAccounts.push(AccountForm.MapFromAccount(this.accounts[i].childAccounts[j].childAccounts[k]));
                }
            }

            if (this.accounts[i].newChildAccounts) {
                for (var j = 0; j < this.accounts[i].newChildAccounts.length; j++) {
                    accountForms[i].newChildAccounts.push(AccountForm.MapFromAccount(this.accounts[i].newChildAccounts[j]));

                    if (this.accounts[i].newChildAccounts[j].newChildAccounts) {
                        for (var k = 0; k < this.accounts[i].newChildAccounts[j].newChildAccounts.length; k++) {
                            accountForms[i].newChildAccounts[j].newChildAccounts.push(AccountForm.MapFromAccount(this.accounts[i].newChildAccounts[j].newChildAccounts[k]));
                        }
                    }
                        
                }
            }
        }

        console.log(accountForms);

        this._server.post('api/account', accountForms).subscribe(
            response => {  },
            error => { }
        )
    }
}
