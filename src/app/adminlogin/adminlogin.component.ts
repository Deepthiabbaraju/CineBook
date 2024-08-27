import { Component,OnInit } from '@angular/core';
import { FormGroup,FormControl,Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
interface LoginResponse {
  token: string;
}
@Component({
  selector: 'app-adminlogin',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './adminlogin.component.html',
  styleUrl: './adminlogin.component.css'
})
export class AdminloginComponent implements OnInit {
  loginForm: FormGroup=new FormGroup({});
  loginError?: string;

  constructor(private http: HttpClient,private router:Router) { }

  ngOnInit(): void {
    this.loginForm = new FormGroup({
      username: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
    });
  }

  onSubmit(): void {
    this.http.post<LoginResponse>('https://localhost:7039/api/admin/login', {
      UserName: this.loginForm.get('username')?.value,
      Password: this.loginForm.get('password')?.value
    })
    .subscribe(response => {
      console.log('Login Response:', response);
      localStorage.setItem('adminAuthToken', response.token);
      this.router.navigate(['/admindashboard']);
    }, error => {
      console.error('Error:', error);
      this.loginError = 'Login failed: ' + 'Incorrect Username or Password';
    });
  }

}
