import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../Models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {

   private apiUrl = 'http://localhost:8090/auth'; // Your backend API URL


    constructor(private http: HttpClient) { } 
      loginWithGoogle(): void {
    window.location.href = `${this.apiUrl}/login`;
  }

  // Get user info after successful login
   getUserInfo(): Observable<User> {
    const token = this.getToken();
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<User>(`${this.apiUrl}/user`, { headers, withCredentials: true });  // Send cookies with request
  }


  // Get the JWT token from cookies
  private getToken(): string | null {
    const token = document.cookie.split('; ').find(row => row.startsWith('jwt='));
    return token ? token.split('=')[1] : null;
  }

  // Check if user is logged in
  isLoggedIn(): boolean {
    return this.getToken() !== null;
  }

  // Logout the user (Clear cookie)
  logout(): void {
    document.cookie = 'jwt=; Max-Age=0; path=/';
  }
}
