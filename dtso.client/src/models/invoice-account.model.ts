import { CityAccount } from './city-account.model'

export class InvoiceAccount {

    invoiceAccountId: number;
    accountId: number;
    cityAccounts: CityAccount[];

    expense: number;


    constructor() {
        this.cityAccounts = [new CityAccount()];
        this.expense = 0;
        this.accountId = undefined;
    }

    static MapFromDetails(details: any): InvoiceAccount {
        var invoiceAccount = new InvoiceAccount();
        invoiceAccount.accountId = details.account.accountId;
        invoiceAccount.expense = details.expense;
        invoiceAccount.invoiceAccountId = details.invoiceAccountId;

        invoiceAccount.cityAccounts = []
        for (var i = 0; i < details.cityExpense.length; i++) {
            invoiceAccount.cityAccounts.push(CityAccount.MapFromDetails(details.cityExpense[i]));
        }

        return invoiceAccount;
    }
    
}