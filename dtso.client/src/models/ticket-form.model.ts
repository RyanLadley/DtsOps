export class TicketForm {

    ticketId: number;
    vendorId: number;
    accountId: number;
    date: Date;
    ticketNumber: string;
    material: any;
    quantity: number;
    cost: number;
    invoiceId: number;
    rawDate: any;
    constructor() {
        this.material = { cost: 0 };
        this.cost = 0;
        this.quantity = 0;
        let now = new Date();
        this.rawDate = { date: { year: now.getFullYear(), month: now.getMonth() + 1, day: now.getDate() } };
    }

    static MapFromDetails(details: any) {
        let form = new TicketForm();

        form.vendorId = details.vendor.vendorId;
        form.accountId = details.account.accountId;
        form.ticketNumber = details.ticketNumber;
        form.date = details.date;
        form.material = {
            materialVendorId: details.material.materialVendorId
        }
        form.cost = details.cost;
        form.ticketId = details.ticketId;
        form.invoiceId = (details.invoice) ? details.invoice.invoiceId : null;
        form.quantity = details.quantity;

        return form;
    }

}