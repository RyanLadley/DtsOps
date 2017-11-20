import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ServerRequest, AuthService } from '../../services/index';
import { TicketForm } from '../../models/index';
@Component({
    selector: 'material-details',
    templateUrl: './material-details.template.html'
})
export class MaterialDetailsComponent implements OnInit {
    editBasics: boolean;
    editTable: boolean;

    material: any;
    tempMaterial: any;
    vendors: any[];
    permissions: any;
    constructor(private _authService: AuthService, private _route: ActivatedRoute, private _server: ServerRequest) {

    }

    ngOnInit() {
        let urlId = "";
        this._route.params.subscribe(params => {
            urlId = params['id']
        });

        this.getMaterial(urlId);
        this.permissions = this._authService.getPermissions();
    }

    getMaterial(urlId) {
        this._server.get('api/material/' + urlId).subscribe(
            response => { this.material = response; console.log(this.material) },
            error => { }
        )
    }

    toggleEditBasics(editing: boolean) {

        this.editBasics = editing;
        if (editing) {
            if (this.editTable) {
                this.toggleEditTable(false);
            }

            this.getEditData();
            this.tempMaterial = JSON.parse(JSON.stringify(this.material));
        }
        else {
            this.material = JSON.parse(JSON.stringify(this.tempMaterial));
        }
    }

    toggleEditTable(editing) {
        this.editTable = editing;
        if (editing) {
            if (this.editBasics) {
                this.toggleEditBasics(false);
            }

            this.getEditData();
            this.tempMaterial = JSON.parse(JSON.stringify(this.material));
        }
        else {
            this.material = JSON.parse(JSON.stringify(this.tempMaterial));
        }
    }

    getEditData() {
        if (!this.vendors) {
            this.getVendors();
        }
        
    }

    getVendors() {
        this._server.get('api/vendor').subscribe(
            response => { console.log(response);this.vendors = response;},
            error => { }
        )
    }
    
    

    submitAdjustment() {

        if (this.editBasics) {
            this.adjustMaterial();
        }
        else {
            this.adjustMaterialVendors();
        }
    }

    addMaterialVendor() {
        this.material.materialVendors.push(
            {
                vendor: {},
                cost: 0,
                canEditVendor: true
            }
        )
    }

    removeMaterialVendor(index: number) {
        this.material.materialVendors.splice(index, 1);
    }

    adjustMaterial() {
        var materialForm = {
            materialId: this.material.materialId,
            name: this.material.name,
            unit: this.material.unit
        }

        this._server.post('api/material/edit', materialForm).subscribe(
            response => {
                this.material = response;
                this.editBasics = false;
            },
            error => { }
        )
    }

    adjustMaterialVendors() {

        var materialForm = {
            materialVendors: []
        }

        for (var i = 0; i < this.material.materialVendors.length; i++) {
            if (this.isVendorUnique(this.material.materialVendors[i].vendor.vendorId)) {
                materialForm.materialVendors.push({
                    materialVendorId: this.material.materialVendors[i].materialVendorId,
                    vendorId: this.material.materialVendors[i].vendor.vendorId,
                    cost: this.material.materialVendors[i].cost,

                    materialId: this.material.materialId,
                    name: this.material.name,
                    unit: this.material.unit
                });
            }
            else
                return;
        }

        this._server.post('api/material/vendor/edit', materialForm).subscribe(
            response => {
                this.material = response;
                this.editTable = false;
            },
            error => { }
        )
    }


    isVendorUnique(vendorId) {
        var found = false;
        for (var i = 0; i < this.material.materialVendors.length; i++){
            if (vendorId == this.material.materialVendors[i].vendor.vendorId) {
                if (!found)
                    found = true;
                else
                    return false
            }
        }
        return true;
        /*
        function checkIfArrayIsUnique(myArray) {
          return myArray.length === new Set(myArray).size;
        }
        */
    }
}
