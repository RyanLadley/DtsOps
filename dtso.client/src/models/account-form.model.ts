

export class AccountForm {

    accountId : number
    accountNumber :string
    subNo :string
    shredNo :string
    description:string
    annualBudget:string

    childAccounts: AccountForm[]
    newChildAccounts: AccountForm[]


    constructor() {
        this.childAccounts = [];
        this.newChildAccounts = []
    }

    static MapFromAccount(account: any) {
        var form = new AccountForm();

        form.accountId = account.accountId;
        form.accountNumber = account.accountNumber;
        form.subNo = account.subNo;
        form.shredNo = account.shredNo;
        form.description = account.description;
        form.annualBudget = account.annualBudget;

        return form;
    }

}