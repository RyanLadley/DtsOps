import { InvoiceAccount } from './invoice-account.model'

export class InvoiceForm {

    invoiceId: number
    vendorId: number;
    invoiceNumber: string;
    invoiceTypeId: number;
    invoiceDate: Date;
    datePaid: Date;
    description: string;

    invoiceAccounts: InvoiceAccount[];


    constructor() {
        this.invoiceAccounts = [new InvoiceAccount()];
        this.invoiceId = undefined;
    }

    static MapFromDetials(details: any) {

        var form = new InvoiceForm();
        form.invoiceId = details.invoiceId;
        form.vendorId = details.vendor.vendorId;
        form.invoiceNumber = details.invoiceNumber;
        form.invoiceTypeId = details.invoiceType.invoiceTypeId;
        form.invoiceDate = details.invoiceDate;
        form.datePaid = details.datePaid;
        form.description = details.description;

        form.invoiceAccounts = [];
        for (var i = 0; i < details.expenses.length; i++) {
            form.invoiceAccounts.push(InvoiceAccount.MapFromDetails(details.expenses[i]));
        }

        return form;
    }

}