﻿export class TicketForm {

    vendorId: number;
    accountId: number;
    date: Date;
    ticketNumber: string;
    material: any;
    quantity: number;
    cost: number;


    constructor() {
        this.material = { cost: 0 };
    }

}