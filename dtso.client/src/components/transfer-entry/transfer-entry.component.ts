import { Component, OnInit, Input } from '@angular/core';
import { ServerRequest } from '../../services/index';
import { TransferForm } from '../../models/index';
@Component({
    selector: 'transfer-entry',
    templateUrl: './transfer-entry.template.html'
})
export class TransferEntryComponent implements OnInit {

    @Input() accounts: any[]
    transfer : TransferForm

    constructor(private _serverRequest: ServerRequest) {

    }

    ngOnInit() {
        this.transfer = new TransferForm();
    }
    
    
}
