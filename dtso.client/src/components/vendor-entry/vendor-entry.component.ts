import { Component, OnInit, Input } from '@angular/core';
import { ServerRequest } from '../../services/index';
import { VendorForm, MaterialKnown, MaterialNew } from '../../models/index';
@Component({
    selector: 'vendor-entry',
    templateUrl: './vendor-entry.template.html'
})
export class VendorEntryComponent implements OnInit {

    @Input() accounts: any[]

    vendor: VendorForm;

    constructor(private _serverRequest: ServerRequest) {

    }

    ngOnInit() {
        this.vendor = new VendorForm();
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
}
