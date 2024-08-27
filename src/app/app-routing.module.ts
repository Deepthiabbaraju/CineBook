import { NgModule } from "@angular/core";
import { RouterModule,Routes } from "@angular/router";
import { SignupComponent } from "./signup/signup.component";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { ProfileComponent } from "./profile/profile.component";
import { MovieDetailComponent } from "./movie-detail/movie-detail.component";
import { BookingComponent } from "./booking/booking.component";
import { SeatselectionComponent } from "./seatselection/seatselection.component";
import { TicketComponent } from "./ticket/ticket.component";
import { EditprofileComponent } from "./editprofile/editprofile.component";
import { AdminloginComponent } from "./adminlogin/adminlogin.component";
import { AdmindashboardComponent } from "./admindashboard/admindashboard.component";
export const routes: Routes=[
    {path:'signup',component:SignupComponent},
    {path:'dashboard',component:DashboardComponent},
    {path:'profile',component:ProfileComponent},
    {path: 'movie/:id', component: MovieDetailComponent },
    {path: 'booking/:movieId',component: BookingComponent},
    {path:'seatselection',component:SeatselectionComponent},
    {path:'ticket',component:TicketComponent},
    {path:'editprofile',component:EditprofileComponent},
    {path:'admin',component:AdminloginComponent},
    {path:'admindashboard',component:AdmindashboardComponent},
    {path:'',redirectTo:'/signup',pathMatch:'full'},
    { path: '**', redirectTo: '/dashboard' }
    
];

@NgModule({
    imports:[RouterModule.forRoot(routes)],
    exports:[RouterModule]
})
export class AppRoutingModule{}
