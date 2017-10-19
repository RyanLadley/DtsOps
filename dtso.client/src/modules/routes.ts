import { Routes } from '@angular/router'
import { HomeComponent, OverviewComponent, AccountDetailsComponent, InvoiceDetailsComponent, DataEntryComponent, VendorOverviewComponent, VendorDetailsComponent, SearchComponent, TicketDetailsComponent } from '../components/index'

export const appRoutes = [
    { path: '', component: HomeComponent },
    { path: 'overview', component: OverviewComponent },
    { path: 'account/:id', component: AccountDetailsComponent },
    { path: 'invoice/:id', component: InvoiceDetailsComponent },
    { path: 'entry', component: DataEntryComponent },
    { path: 'vendor', component: VendorOverviewComponent },
    { path: 'vendor/:id', component: VendorDetailsComponent },
    { path: 'search', component: SearchComponent },
    { path: 'ticket/:id', component: TicketDetailsComponent }
];