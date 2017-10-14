import { Component, OnInit } from '@angular/core';
import { MonthProvider} from '../../services/index';
@Component({
    selector: 'account-details',
    templateUrl: './account-details.template.html'
})
export class AccountDetailsComponent implements OnInit {
    collapse: boolean;
    selectedMonth: number;
    displayedInvoices: any;
    months: string[]

    constructor(private _monthProvider : MonthProvider) {

    }

    ngOnInit() {
        this.months = this._monthProvider.monthList();

        //Default to Current Month
        this.selectedMonth = 8 //new Date().getMonth
        this.displayedInvoices = this.account.monthlyDetails[this.selectedMonth].invoices
        console.log(this.displayedInvoices)
    }
    












    account: any =
    {
        "subNo": null,
        "shredNo": null,
        "description": "In House Resurfacing",
        "annualBudget": 2,
        "transfers": 0,
        "accountNumber": 5221000,
        "expedituresToDate": 65.23,
        "monthlyDetails": {
            "1": {
                "month": 1,
                "invoices": [],
                "totalExpense": 0
            },
            "2": {
                "month": 2,
                "invoices": [],
                "totalExpense": 0
            },
            "3": {
                "month": 3,
                "invoices": [],
                "totalExpense": 0
            },
            "4": {
                "month": 4,
                "invoices": [],
                "totalExpense": 0
            },
            "5": {
                "month": 5,
                "invoices": [],
                "totalExpense": 0
            },
            "6": {
                "month": 6,
                "invoices": [],
                "totalExpense": 0
            },
            "7": {
                "month": 7,
                "invoices": [],
                "totalExpense": 0
            },
            "8": {
                "month": 8,
                "invoices": [
                    {
                        "invoiceId": 2,
                        "invoiceNumber": "17POS/092790A",
                        "accountNumber": "5221000",
                        "expense": 20,
                        "vendor": {
                            "vendorId": 1,
                            "name": "Grainger"
                        },
                        "invoiceType": "Equipment",
                        "invoiceDate": "2017-08-23T12:30:00",
                        "description": "Donec vestibulum convallis tortor"
                    },
                    {
                        "invoiceId": 2,
                        "invoiceNumber": "17POS/092790A",
                        "accountNumber": "5221000-3-4",
                        "expense": 45.23,
                        "vendor": {
                            "vendorId": 1,
                            "name": "Grainger"
                        },
                        "invoiceType": "Equipment",
                        "invoiceDate": "2017-08-23T12:30:00",
                        "description": "unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, "
                    },
                    {
                        "invoiceId": 3,
                        "invoiceNumber": "10007558012",
                        "accountNumber": "5221000",
                        "expense": 578,
                        "vendor": {
                            "vendorId": 1,
                            "name": "Grainger"
                        },
                        "invoiceType": "Equipment",
                        "invoiceDate": "2017-08-23T12:30:00",
                        "description": "Etiam tincidunt cursus ipsum, vitae consequa"
                    },
                    {
                        "invoiceId": 3,
                        "invoiceNumber": "10007558012",
                        "accountNumber": "5221000-3-4",
                        "expense": 545.23,
                        "vendor": {
                            "vendorId": 1,
                            "name": "Grainger"
                        },
                        "invoiceType": "Equipment",
                        "invoiceDate": "2017-08-23T12:30:00",
                        "description": "First"
                    }
                ],
                "totalExpense": 65.23
            },
            "9": {
                "month": 9,
                "invoices": [],
                "totalExpense": 0
            },
            "10": {
                "month": 10,
                "invoices": [],
                "totalExpense": 0
            },
            "11": {
                "month": 11,
                "invoices": [],
                "totalExpense": 0
            },
            "12": {
                "month": 12,
                "invoices": [],
                "totalExpense": 0
            }
        },
        "totalBudget": 1028.20,
        "remainingBalance": -63.23
    }
}
