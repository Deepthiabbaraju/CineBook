import { Component,OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators,ReactiveFormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
@Component({
  selector: 'app-editprofile',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './editprofile.component.html',
  styleUrl: './editprofile.component.css'
})
export class EditprofileComponent implements OnInit{
  message?:string;
  editProfileForm: FormGroup=new FormGroup({});
  constructor(private http: HttpClient, private router:Router) { }

  ngOnInit(): void {
    this.editProfileForm = new FormGroup({
      email: new FormControl('', Validators.required),
      currentPassword: new FormControl('',Validators.required),
      password: new FormControl('', Validators.required),
      location: new FormControl('', Validators.required),
      preferredLanguage: new FormControl('', Validators.required)
    });

    this.populateFormFields();
  }

  populateFormFields(): void {
    const storedEmail = (localStorage).getItem('userName');
    const storedLocation = localStorage?.getItem('userLocation');
    const storedPreferredLanguage = localStorage?.getItem('language');
    console.log(storedEmail);

    if (storedEmail) this.editProfileForm.get('email')!.setValue(storedEmail);
    if (storedLocation) this.editProfileForm.get('location')!.setValue(storedLocation);
    if (storedPreferredLanguage) this.editProfileForm.get('preferredLanguage')!.setValue(storedPreferredLanguage);
  }

  onSubmit(): void {
    if (this.editProfileForm.valid) {
      const editedEmail = this.editProfileForm.get('email')!.value;
      const editedLocation = this.editProfileForm.get('location')!.value;
      const editedPreferredLanguage = this.editProfileForm.get('preferredLanguage')!.value;
      const editedPassword = this.editProfileForm.get('password')!.value;
      const currentPassword = this.editProfileForm.get('currentPassword')?.value;
      const storedPassword = localStorage.getItem('password'); // Assuming you store the password here
      console.log(storedPassword);
      if (currentPassword === storedPassword) {
        const userData = {
            id: localStorage.getItem('userId'),
            UserName: editedEmail,
            Location: editedLocation,
            PreferredLanguage: editedPreferredLanguage,
            Password: editedPassword
        };

        this.http.put<any>(`https://localhost:7039/api/Users/${userData.id}`, userData)
            .subscribe(
                response => {
                        localStorage.setItem('userEmail', editedEmail);
                        localStorage.setItem('password', editedPassword);
                        localStorage.setItem('userLocation', editedLocation);
                        localStorage.setItem('userPreferredLanguage', editedPreferredLanguage);
                        this.message = 'Profile updated successfully!';
                        setTimeout(() => {
                          this.router.navigate(['/dashboard']);
                        }, 2000); 
                    
                },
                error => {
                   this.message='Failed to update profile' ;
                }
            );
    } else {
        this.message='Current password does not match.';
    }
    }
  }

  cancelEdit(): void {
    this.router.navigate(['/dashboard']);
  }


}
