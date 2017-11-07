import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ServerRequest } from '../../services/index';
import { TicketForm } from '../../models/index';
@Component({
    selector: 'material-details',
    templateUrl: './material-details.template.html'
})
export class MaterialDetailsComponent implements OnInit {
    editBasics: boolean;

    material: any;
    tempMaterial: any;

    vendors: any[];

    constructor(private _route: ActivatedRoute, private _server: ServerRequest) {

    }

    ngOnInit() {
        let urlId = "";
        this._route.params.subscribe(params => {
            urlId = params['id']
        });

        this.getMaterial(urlId);
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
        this._server.get('api/vendor?withMaterials=true').subscribe(
            response => { this.vendors = response;},
            error => { }
        )
    }
    
    

    submitTicketAdjustment(){
        var ticketForm = TicketForm.MapFromDetails(this.material);
        

        this._server.post('api/material/edit', ticketForm).subscribe(
            response => {
                this.material = response;
                this.editBasics = false;
            },
            error => { }
        )
    }
}
