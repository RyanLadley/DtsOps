import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import {
    AppRoot,
    SidebarComponent,
    HomeComponent,
    OverviewComponent,
    AccountDetailsComponent,
    InvoiceDetailsComponent,
    DataEntryComponent,
    InvoiceEntryComponent,
    VendorEntryComponent,
    TicketEntryComponent,
    TransferEntryComponent,
    VendorOverviewComponent,
    VendorDetailsComponent,
    AccountSelectComponent
} from '../components/index';

import {
    AuthService,
    ServerRequest,
    TokenManager,
    ObjectSort,
    MonthProvider
} from '../services/index';

import {
    AccountNamePipe
} from '../pipes/index';

import { appRoutes } from './routes';

import { AppSettings } from '../settings/appsettings'

@NgModule({
  declarations: [
      AppRoot,
      SidebarComponent,
      HomeComponent,
      OverviewComponent,
      AccountDetailsComponent,
      InvoiceDetailsComponent,
      DataEntryComponent,
      InvoiceEntryComponent,
      VendorEntryComponent,
      TicketEntryComponent,
      AccountSelectComponent,
      TransferEntryComponent,
      VendorOverviewComponent,
      VendorDetailsComponent,
      //Pipes
      AccountNamePipe
  ],
  imports: [
      BrowserModule,
      HttpModule,
      FormsModule,
      ReactiveFormsModule,
      RouterModule.forRoot(appRoutes, { useHash: true })
  ],
  providers: [
      AppSettings,
      ServerRequest,
      TokenManager,
      AuthService,
      ObjectSort,
      MonthProvider
  ],
  bootstrap: [AppRoot]
})
export class AppModule { }
