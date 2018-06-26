import { Component, OnInit } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router'
import { ServerRequest, ArraySort } from '../../services/index';
import { VendorForm, MaterialKnown, MaterialNew } from "../../models/index";
import { AuthService } from "../../services/auth.service";

@Component({
  selector: 'vendor-details',
  templateUrl: './vendor-details.template.html'
})
export class VendorDetailsComponent implements OnInit {

    displayedBreakdown: string;
    vendor: any;
    tempVendor: any;
    editBasics: boolean
    permissions: any;
    errorMessage: string;
    errorMessageTable: string;
    materials: any;
    
    invoicesTotal: number;
    
    ticketsTotal: number;
    pendingTotal: number;

    //Datepicker Values
    contractStart: any;
    contractEnd: any;

    sortInvoiceAscending: boolean;
    currentInvoiceSort: string;

    sortTicketAscending: boolean;
    currentTicketSort: string;

    editTable: boolean;
    vendorForm: VendorForm;

    constructor(private _authService: AuthService, private _route: ActivatedRoute, private _router: Router, private _server: ServerRequest, private _sorter: ArraySort) {
        
    }

    ngOnInit() {
        let urlId = "";
        this._route.params.subscribe(params => {
            urlId = params['id']
        });

        this.getVendor(urlId);
        this.getMaterials();
        this.permissions = this._authService.getPermissions();

        this.currentInvoiceSort = "accountNumber";
        this.currentTicketSort = "account";
    }

    toggleEditBasics(editing: boolean) {

        this.editBasics = editing;
        if (editing) {

            //Initiazlie Date Picker Dates
            let end = new Date(this.vendor.contractEnd)
            this.contractEnd = { date: { year: end.getFullYear(), month: end.getMonth() + 1, day: end.getDate() } };

            let start = new Date(this.vendor.contractStart)
            this.contractStart = { date: { year: start.getFullYear(), month: start.getMonth() + 1, day: start.getDate() } };

            this.tempVendor = JSON.parse(JSON.stringify(this.vendor));
        }
        else {
            this.errorMessage = null;
            this.vendor = JSON.parse(JSON.stringify(this.tempVendor));
        }
    }

    toggleEditTable(editing: boolean) {

        this.editTable = editing;
        if (editing) {
            if (this.editBasics) {
                this.toggleEditBasics(false);
            }
            this.tempVendor = JSON.parse(JSON.stringify(this.vendor));

            this.vendorForm = new VendorForm();
            this.vendorForm.vendorId = this.vendor.vendorId;
            this.vendorForm.knownMaterial = this.vendor.materials
        }
        else {
            this.errorMessageTable = null;
            this.vendor = JSON.parse(JSON.stringify(this.tempVendor));
        }
    }

    setDisplayedBreakdown(displayed) {
        this.displayedBreakdown = displayed;

    }

    selectFilter(filter: string) {
        
    }

    getStatusIcon(status: string) {
    }

    addKnownMaterial() {
        this.vendorForm.knownMaterial.push(new MaterialKnown());
    }

    removeKnownMaterial(index: number) {
        this.vendorForm.knownMaterial.splice(index, 1);
    }

    addNewMaterial() {
        this.vendorForm.newMaterial.push(new MaterialNew());
    }

    removeNewMaterial(index: number) {
        this.vendorForm.newMaterial.splice(index, 1);
    }

    selectKnownMaterial(materialId: number, index: number) {
        for (var i = 0; i < this.materials.length; i++) {
            if (this.materials[i].materialId == materialId) {
                this.vendorForm.knownMaterial[index].unit = this.materials[i].unit;
            }
        }
    }

    getMaterials() {
        this._server.get('api/material').subscribe(
            response => { this.materials = response;},
            error => { }
        )
    }

    getVendor(id: string) {
        this._server.get('api/vendor/' + id).subscribe(
            response => {
                this.vendor = response;
                this.displayedBreakdown = "Invoice";
                this.processVendorFromServer() 
            },
            error => {}
        )
    }

    alterStatus(active: boolean) {
        if (active) {
            this.vendor.status="Active"
        }
        else {
            this.vendor.status = "Inactive";
        }
    }
    //This is a function rather than a router link so that we can preform a check if we are editing: we dont want to leave if we are editing
    gotoMaterial(materialId) {
        this._router.navigate(['material/' + materialId]);
    }

    submitAdjustment() {
        var vendorForm = VendorForm.MapFromDetails(this.vendor);

        if (this.contractStart == null) {
            this.errorMessage = "A contract start date is required.";
            return
        }
        if (this.contractEnd == null) {
            this.errorMessage = "A contract end date is required.";
            return
        }
        vendorForm.contractStart = new Date(this.contractStart.date.year, this.contractStart.date.month - 1, this.contractStart.date.day);
        vendorForm.contractEnd = new Date(this.contractEnd.date.year, this.contractEnd.date.month - 1, this.contractEnd.date.day);

        this._server.post('api/vendor/edit', vendorForm).subscribe(
            response => {
                this.getMaterials(); //Just in case new materails were added, we should update the list
                this.sortInvoiceAscending = !this.sortInvoiceAscending // This is so we dont switch the direction of the ascent when processing
                this.processVendorFromServer()
                this.toggleEditBasics(false);
                this.vendor = response;
            },
            error => { this.errorMessage = error  }
        )
    }

    submitTableAdjustment() {
        
        this._server.post('api/vendor/materials', this.vendorForm).subscribe(
            response => {
                this.sortInvoiceAscending = !this.sortInvoiceAscending // This is so we dont switch the direction of the ascent when processing
                this.processVendorFromServer()
                this.toggleEditTable(false);
                this.vendor = response;
            },
            error => { this.errorMessageTable = error }
        )
    }

    processVendorFromServer() {
        this.caluclateTotals();
        this.sortInvoicesBy(this.currentInvoiceSort);
        this.sortTicketsBy(this.currentTicketSort);
    }

    getInvoiceSortIcon(field) {
        if (field == this.currentInvoiceSort) {
            if (this.sortInvoiceAscending) {
                return "fa-caret-up";
            }
            else {
                return "fa-caret-down";
            }
        }
    }

    sortInvoicesBy(field) {
        //Set the asscention direction. If this is a new feild, it is defaulted to ascending, if the user clicked the same field, we toggle the direction
        var direction: boolean;
        if (field == this.currentInvoiceSort) {
            direction = !this.sortInvoiceAscending
        }
        else {
            direction = true;
        }

        this.currentInvoiceSort = field;
        this.sortInvoiceAscending = direction;

        this._sorter.sort(this.vendor.invoices, field, direction);
    }

    getTicketSortIcon(field) {
        if (field == this.currentTicketSort) {
            if (this.sortTicketAscending) {
                return "fa-caret-up";
            }
            else {
                return "fa-caret-down";
            }
        }
    }

    sortTicketsBy(field) {
        //Set the asscention direction. If this is a new feild, it is defaulted to ascending, if the user clicked the same field, we toggle the direction
        var direction: boolean;
        if (field == this.currentTicketSort) {
            direction = !this.sortTicketAscending
        }
        else {
            direction = true;
        }

        this.currentTicketSort = field;
        this.sortTicketAscending = direction;

        this._sorter.sort(this.vendor.tickets, field, direction);
    }


    caluclateTotals() {
        //Invoice Totals
        this.invoicesTotal = 0
        for (var i = 0; i < this.vendor.invoices.length; i++) {
            this.invoicesTotal += this.vendor.invoices[i].expense;
        }

        //TIcket Totals
        this.ticketsTotal = 0;
        this.pendingTotal = 0;
        for (var i = 0; i < this.vendor.tickets.length; i++) {
            this.ticketsTotal += this.vendor.tickets[i].cost;

            if (!this.vendor.tickets[i].invoice) {
                this.pendingTotal += this.vendor.tickets[i].cost;
            }
        }
        
    }
}
