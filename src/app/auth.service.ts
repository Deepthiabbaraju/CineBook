import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  

  constructor(private http:HttpClient) { }
  login(email1:string,password1:string):Observable<any>{
    const userData={
      UserName:email1,
      Password:password1
    };
    return this.http.post('https://localhost:7039/api/Users/login',userData);
  }
  signup(name:string,email:string,password:string,location:string,language:string):Observable<any>{
    const userData = {
      id:name,
      UserName: email,
      Password: password,
      Location: location,
      PreferredLanguage: language
    };
    return this.http.post('https://localhost:7039/api/Users/register', userData);
  }

}
