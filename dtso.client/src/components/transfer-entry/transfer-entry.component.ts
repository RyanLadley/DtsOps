import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ServerRequest } from '../../services/index';
import { TransferForm } from '../../models/index';
@Component({
    selector: 'transfer-entry',
    templateUrl: './transfer-entry.template.html'
})
export class TransferEntryComponent implements OnInit {

    @Input() accounts: any[]
    transfer : TransferForm
    errorMessage: string;
    constructor(private _server: ServerRequest, private _router: Router) {

    }

    ngOnInit() {
        this.transfer = new TransferForm();
    }

    submitNewTransfer() {
        //Add validation and stuff. Have fun future me
        
        //Assign the selected accpunt and vendor ids to the tickets

        this._server.post("api/transfer", this.transfer).subscribe(
            response => { this._router.navigate(['/overview']) },
            error => { this.errorMessage = error }
        )
    }
    
}
