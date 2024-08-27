import { Component,OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule,FormBuilder,FormGroup,Validators,FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { IonicStorageModule } from '@ionic/storage-angular';
import { RouterModule } from '@angular/router';
@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,RouterModule],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.css'
})
export class SignupComponent implements OnInit {
  showSignup = true;
  signupForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required, Validators.minLength(6)]),
    location: new FormControl('', [Validators.required]),
    language: new FormControl('', [Validators.required])
  });

  loginForm = new FormGroup({
    email1: new FormControl('', [Validators.required, Validators.email]),
    password1: new FormControl('', [Validators.required, Validators.minLength(6)])
  });

  constructor(private authService: AuthService, private router:Router) { }

  ngOnInit(): void {
  }
  showLoginForm() {
    this.showSignup = false;
  }

  showSignupForm() {
    this.showSignup = true;
  }

  toggleForm() {
    this.showSignup = !this.showSignup;
  }

  onSubmit() {
    this.authService.signup(
      this.signupForm.get('name')?.value ?? '',
      this.signupForm.get('email')?.value ?? '',
      this.signupForm.get('password')?.value ?? '',
      this.signupForm.get('location')?.value ?? '',
      this.signupForm.get('language')?.value ?? ''
    ).subscribe(response => {
      console.log(response);
      localStorage.setItem('authToken', response.token);
      localStorage.setItem('location', response.location);
      localStorage.setItem('userId', response.id);
      this.router.navigate(['/dashboard']);
    });
  }

  onLoginSubmit() {
    this.authService.login(
      this.loginForm.get('email1')?.value ?? '',
      this.loginForm.get('password1')?.value ?? ''
    ).subscribe(response => {
      console.log(response);
      localStorage.setItem('authToken', response.token);
      localStorage.setItem('location', response.location);
      localStorage.setItem('userId', response.id);
      this.router.navigate(['/dashboard']);
    });
  }
}
