import { Component,OnInit} from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
  userData:any;
  authToken:string='';
  userId?:string='';
  constructor(private http: HttpClient, private router: Router){ }
  ngOnInit(): void {
    this.authToken=localStorage.getItem('authToken')?? '';
    this.userId=localStorage.getItem('userId') ?? '';
    if (!this.authToken|| !this.userId){
      this.router.navigate(['/signup']);
      return;
    }
    this.fetchUserData();
  }
  fetchUserData():void{
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.authToken}`,
      'Content-Type': 'application/json'
    });

    this.http.get(`https://localhost:7039/api/Users/${this.userId}`, { headers })
      .subscribe(response => {
        this.userData = response;
        localStorage.setItem('userId', this.userData.id);
        localStorage.setItem('userLocation', this.userData.location);
        localStorage.setItem('userName',this.userData.userName);
        localStorage.setItem('password',this.userData.password);
        localStorage.setItem('language',this.userData.preferredLanguage);
        console.log(this.userData);
        console.log('userlocation', this.userData.location);
        console.log('language', this.userData.preferredLanguage);
      }, error => {
        console.error('Error fetching user data:', error);
      });
  }
  editProfile(): void {
    localStorage.setItem('returnTo', 'dashboard');
    this.router.navigate(['/editprofile']);
  }

}
