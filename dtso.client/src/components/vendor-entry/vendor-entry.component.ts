﻿import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ServerRequest } from '../../services/index';
import { VendorForm, MaterialKnown, MaterialNew } from '../../models/index';
@Component({
    selector: 'vendor-entry',
    templateUrl: './vendor-entry.template.html'
})
export class VendorEntryComponent implements OnInit {

    @Input() accounts: any[];
    @Input() materials: any[];
    vendor: VendorForm;
    errorMessage: string;
    contractStart: any;
    contractEnd: any;

    constructor(private _router: Router, private _server: ServerRequest) {

    }

    ngOnInit() {
        this.vendor = new VendorForm();
        let now = new Date();
        this.contractStart = { date: { year: now.getFullYear(), month: now.getMonth() + 1, day: now.getDate() } };
        this.contractEnd = { date: { year: now.getFullYear(), month: now.getMonth() + 1, day: now.getDate() } };
    }

    addKnownMaterial() {
        this.vendor.knownMaterial.push(new MaterialKnown());
    }

    removeKnownMaterial(index: number) {
        this.vendor.knownMaterial.splice(index, 1);
    }

    addNewMaterial() {
        this.vendor.newMaterial.push(new MaterialNew());
    }

    removeNewMaterial(index: number) {
        this.vendor.newMaterial.splice(index, 1);
    }

    selectKnownMaterial(materialId: number, index: number) {
        for (var i = 0; i < this.materials.length; i++) {
            if (this.materials[i].materialId == materialId) {
                this.vendor.knownMaterial[index].unit = this.materials[i].unit;
            }
        }
    }

    submitNewVendor() {
        this.vendor.contractStart = new Date(this.contractStart.date.year, this.contractStart.date.month - 1, this.contractStart.date.day);
        this.vendor.contractEnd = new Date(this.contractEnd.date.year, this.contractEnd.date.month - 1, this.contractEnd.date.day);
        this._server.post("api/vendor", this.vendor).subscribe(
            response => { this._router.navigate(['/vendor', response.vendorId]); },
            error => { this.errorMessage = error }
        )
    }

}
