import { CityAccount } from './city-account.model'

export class InvoiceAccount {

    accountId: number;
    cityAccounts: CityAccount[];

    expense: number;


    constructor() {
        this.cityAccounts = [new CityAccount()];
        this.expense = 0;
        this.accountId = undefined;
    }
    
}