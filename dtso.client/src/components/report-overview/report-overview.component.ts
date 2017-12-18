import { Component, OnInit } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router'
import { ServerRequest, ArraySort } from '../../services/index';

@Component({
  selector: 'report-overview',
  templateUrl: './report-overview.template.html'
})
export class ReportOverviewComponent implements OnInit {
    

    constructor(private _route: ActivatedRoute, private _router: Router, private _server: ServerRequest) {
        
    }

    ngOnInit() {

    }
    
}
