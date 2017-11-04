import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges  } from '@angular/core';

@Component({
  selector: 'account-select',
  templateUrl: './account-select.template.html'
})

//This componenet creates a series of dropdowns for each layer of account (account, sub account, and shred account)
//This is used to fcacilitate the selection of accounts in data entry nad adjustments 
export class AccountSelectComponent implements OnChanges{
    @Input() accounts: any;
    @Output() selectedAccountIdChange: EventEmitter<number> = new EventEmitter();
    @Input() selectedAccountId: number;
    subAccounts: any[];
    shredAccounts: any[];

    selectedAccount: any;
    selectedSubAccount: any;
    selectedShredAccount: any;
    constructor() {

    }
    
    ngOnChanges(changes: SimpleChanges) {
        this.refreshDisplay();
    }

    populateSubAccounts(account) {
        this.selectedAccountId = account.accountId;
        this.selectedAccountIdChange.emit(account.accountId);
        
        this.selectedAccount = account;
        this.subAccounts = [];
        if (account.childAccounts.length > 0) {
            this.subAccounts = account.childAccounts
        }
    }

    populateShredAccounts(subAccount) {
        if (subAccount == undefined) {
            this.selectedAccountId = this.selectedAccount.accountId;
            this.selectedAccountIdChange.emit(this.selectedAccount.accountId);
        }
        else {
            this.selectedAccountId = subAccount.accountId;
            this.selectedAccountIdChange.emit(subAccount.accountId);

            this.selectedSubAccount = subAccount;
            this.shredAccounts = []
            if (subAccount.childAccounts.length > 0) {
                this.shredAccounts = subAccount.childAccounts;
            }
        }
    }

    asssignShredout(shredAccount) {
        if (shredAccount == undefined) {
            this.selectedAccountId = this.selectedSubAccount.accountId;
            this.selectedAccountIdChange.emit(this.selectedSubAccount.accountId);
        }
        else {
            this.selectedAccountId = shredAccount.accountId;
            this.selectedAccountIdChange.emit(shredAccount.accountId);
        }

        this.selectedShredAccount = shredAccount;
    }


    //If an accountId is provided, this block will find it and display the proper dropdowns
    //for the user. 
    //TODO: Make this less terrible. This is run everytime the accountID is changed to allow for 
    //      for an accountId to be provided. Find a way to remove provided accountId with user selected.
    //      The use of two variables might be useful for this.
    refreshDisplay() {
        if (this.accounts && this.selectedAccountId) {
            let accountFound = false;

            for (var i = 0; i < this.accounts.length; i++) {
                if (this.accounts[i].accountId == this.selectedAccountId) {
                    this.selectedAccount = this.accounts[i]
                    this.populateSubAccounts(this.accounts[i])
                    accountFound = true
                    break;
                }
                else {
                    for (var j = 0; j < this.accounts[i].childAccounts.length; j++) {
                        if (this.accounts[i].childAccounts[j].accountId == this.selectedAccountId) {
                            this.selectedAccount = this.accounts[i];
                            this.populateSubAccounts(this.accounts[i]);
                            this.selectedSubAccount = this.accounts[i].childAccounts[j];
                            this.populateShredAccounts(this.accounts[i].childAccounts[j])
                            accountFound = true
                            break;
                        }
                        else {
                            for (var k = 0; k < this.accounts[i].childAccounts[j].childAccounts.length; k++) {
                                if (this.accounts[i].childAccounts[j].childAccounts[k].accountId == this.selectedAccountId) {
                                    this.selectedAccount = this.accounts[i]
                                    this.populateSubAccounts(this.accounts[i])
                                    this.selectedSubAccount = this.accounts[i].childAccounts[j]
                                    this.populateShredAccounts(this.accounts[i].childAccounts[j])
                                    this.asssignShredout(this.accounts[i].childAccounts[j].childAccounts[k])
                                    accountFound = true
                                    break;
                                }
                            }

                            if (accountFound)
                                break;
                        }
                    }

                    if (accountFound)
                        break;
                }
            }
        }
    }
}
