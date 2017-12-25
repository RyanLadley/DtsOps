

export class AccountForm {

    accountId : number
    accountNumber :number
    subNo: number
    shredNo: number
    description:string
    annualBudget: string
    parentId: number;
    
    
    constructor() {
    }

    static MapFromAccount(account: any, parentId: number) {
        var form = new AccountForm();

        form.accountId = account.accountId;
        form.accountNumber = account.accountNumber;
        form.subNo = account.subNo;
        form.shredNo = account.shredNo;
        form.description = account.description;
        form.annualBudget = account.annualBudget;
        form.parentId = parentId;
        return form;
    }

}