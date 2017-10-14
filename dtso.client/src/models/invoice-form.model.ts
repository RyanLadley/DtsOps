import { InvoiceAccount } from './invoice-account.model'

export class InvoiceForm {

    vendorId: number;
    invoiceNumber: string;
    trasactionTypeId: number;
    invoiceDate: Date;
    datePaid: Date;
    descripion: string;

    invoiceAccounts: InvoiceAccount[];


    constructor() {
        this.invoiceAccounts = [new InvoiceAccount()];
    }

}