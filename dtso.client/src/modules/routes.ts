import { Routes } from '@angular/router'
import {
    HomeComponent,
    LoginComponent,
    OverviewComponent,
    AccountDetailsComponent,
    InvoiceDetailsComponent,
    DataEntryComponent,
    VendorOverviewComponent,
    VendorDetailsComponent,
    SearchComponent,
    TicketDetailsComponent,
    MaterialDetailsComponent,
    ReportOverviewComponent,
    ProfileComponent
} from '../components/index'

export const appRoutes = [
    { path: '', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'overview', component: OverviewComponent },
    { path: 'account/:id', component: AccountDetailsComponent },
    { path: 'invoice/:id', component: InvoiceDetailsComponent },
    { path: 'entry', component: DataEntryComponent },
    { path: 'vendor', component: VendorOverviewComponent },
    { path: 'vendor/:id', component: VendorDetailsComponent },
    { path: 'search', component: SearchComponent },
    { path: 'ticket/:id', component: TicketDetailsComponent },
    { path: 'material/:id', component: MaterialDetailsComponent },
    { path: 'report', component: ReportOverviewComponent },
    { path: 'profile', component: ProfileComponent }
];