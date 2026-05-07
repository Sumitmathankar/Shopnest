import { Routes } from '@angular/router';
import { HomepageComponent } from './core/homepage/homepage.component';
import { ProductpageComponent } from './core/productpage/productpage.component';

export const routes: Routes = [
    {path: '', redirectTo: 'home', pathMatch: 'full'},
    {path: 'home', component: HomepageComponent},
    {path: 'products/:category', component: ProductpageComponent },

];

