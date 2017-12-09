import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { CookieModule } from 'ngx-cookie';

import {
    AppRoot,
    LoginComponent,
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
    AccountSelectComponent,
    TicketDetailsComponent,
    SearchComponent,
    MaterialDetailsComponent,
    ProfileComponent,
    LoadingComponent
} from '../components/index';

import {
    AuthService,
    ServerRequest,
    CookieManager,
    ArraySort,
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
      LoginComponent,
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
      SearchComponent,
      MaterialDetailsComponent,
      TicketDetailsComponent,
      ProfileComponent,
      LoadingComponent,
      //Pipes
      AccountNamePipe
  ],
  imports: [
      BrowserModule,
      HttpModule,
      FormsModule,
      ReactiveFormsModule,
      CookieModule.forRoot(),
      RouterModule.forRoot(appRoutes, { useHash: true })
  ],
  providers: [
      AppSettings,
      ServerRequest,
      CookieManager,
      AuthService,
      ArraySort,
      MonthProvider
  ],
  bootstrap: [AppRoot]
})
export class AppModule { }
