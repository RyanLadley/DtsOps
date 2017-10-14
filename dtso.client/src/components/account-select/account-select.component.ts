import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'account-select',
  templateUrl: './account-select.template.html'
})

//This componenet creates a series of dropdowns for each layer of account (account, sub account, and shred account)
//This is used to fcacilitate the selection of accounts in data entry nad adjustments 
export class AccountSelectComponent {
    @Input() accounts: any;
    @Output() selectedAccountId: EventEmitter<number> = new EventEmitter();
    subAccounts: any[];
    shredAccounts: any[];

    selectedAccount: any;
    selectedSubAccount: any;
    selectedShredAccount: any;
    constructor() {

    }

    populateSubAccounts(account) {
        this.selectedAccountId.emit(account.accountId);

        this.selectedAccount = account;
        this.subAccounts = [];
        if (account.childAccounts.length > 0) {
            this.subAccounts = account.childAccounts
        }
    }

    populateShredAccounts(subAccount) {
        console.log(subAccount)
        if (subAccount == undefined)
            this.selectedAccountId.emit(this.selectedAccount.accountId);
        else
            this.selectedAccountId.emit(subAccount.accountId);

        this.selectedSubAccount = subAccount;
        this.shredAccounts = []
        if (subAccount.childAccounts.length > 0) {
            this.shredAccounts = subAccount.childAccounts;
        }
    }

    asssignShredout(shredAccount) {
        if (shredAccount == undefined)
            this.selectedAccountId.emit(this.selectedSubAccount.accountId);
        else
            this.selectedAccountId.emit(shredAccount.accountId);

        this.selectedShredAccount = shredAccount;
    }
}
