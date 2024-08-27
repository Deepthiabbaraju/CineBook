import { Component,OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CommonModule} from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
export interface Movie {
  id: number;
  title: string;
  genre: string;
  releaseDate: Date;
  language: string;
  location: string;
  imageUrl: string;
  duration: string;
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterModule,FormsModule,CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit{
  searchQuery = '';
  movies:Movie[]=[];
  newLocation = '';
  currentLocation = localStorage.getItem('location') || 'default location';
  popularCities = ['Mumbai', 'Delhi-NCR', 'Bengaluru', 'Hyderabad', 'Ahmedabad', 'Chandigarh', 'Chennai', 'Pune', 'Kolkata', 'Kochi'];
  locationModalOpen = false;
  userId?:string;
  authToken?:string;
  msg?:string;
  constructor(private http: HttpClient, private router: Router) { }
  
  ngOnInit(): void {

    this.userId =  localStorage.getItem('userId') ?? '';
    this.authToken = localStorage.getItem('authToken') ?? '';
    const storedLocation = localStorage.getItem('newLocation') || this.currentLocation;
    if (!this.authToken) {
      this.logout();
      return;
    }

    if (storedLocation) {
        this.fetchMovies(storedLocation);
    } else {
        this.fetchUserData().then((location) => {
            this.fetchMovies(location);
        });
    }
  }

  fetchUserData(): Promise<string> {
    return this.http.get(`https://localhost:7039/api/Users/${localStorage.getItem('userId')}`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('authToken')}`,
        'Content-Type': 'application/json'
      }
    })
      .toPromise()
      .then((response: any) => {
        localStorage.setItem('userData', JSON.stringify(response));
        return response.location;
      })
      .catch((error: any) => {
        console.error('Error fetching user data:', error);
      });
  }
  fetchMovies(location: string) {
    this.http.get<Movie[]>(`https://localhost:7039/api/movies?location=${location}`)
      .subscribe(
        (movies: Movie[]) => {
          this.movies = movies;
          if (movies.length === 0) {
            this.msg = `No movies found at "${location}"`;
          } else {
            this.msg = '';
          }
        },
        (error) => {
          if (error.status === 404) {
            this.msg = `No movies found at "${location}"`;
          } else {
            this.msg = 'An error occurred while fetching movies.';
          }
          this.movies = [];
        }
      );
  }

  

  searchMovies(): void {
    const searchQuery = this.searchQuery.trim();
    const userLocation = localStorage.getItem('newLocation') || this.currentLocation;
    if (searchQuery && userLocation) {
      this.http.get(`https://localhost:7039/api/movies/search?query=${encodeURIComponent(searchQuery)}&location=${encodeURIComponent(userLocation)}`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('authToken')}`,
          'Content-Type': 'application/json'
        }
      })
      .toPromise()
      .then((response: any) => {
        this.movies = response;
        if (this.movies.length === 0) {
          this.msg = `No movies found for "${searchQuery}" at "${userLocation}"`;
        } else {
          this.msg = '';
        }
      })
      .catch((error: any) => {
        console.error('Error searching for movies:', error);
      });
  } else {
    this.movies = [];
    this.msg = `No movies found for search query "${searchQuery}"`;
  }
  }
  locationModal(): void {
    console.log("locationModal called");
    this.locationModalOpen = true;
  }

  closeLocationModal(): void {
    this.locationModalOpen = false;
  }
  submitLocation(): void {
    localStorage.setItem('newLocation', this.newLocation);
    this.fetchMovies(this.newLocation);
    this.closeLocationModal();
  }

  selectCity(city: string): void {
    this.newLocation = city;
    this.submitLocation();
  }
  profile(){
    if (!this.userId || !this.authToken) return;
    localStorage.setItem('userId',this.userId);
    localStorage.setItem('authToken',this.authToken);
    this.router.navigate(['/profile']);

  }

  logout(): void {
    localStorage.removeItem('authToken');
    localStorage.removeItem('userId');
    localStorage.removeItem('userData');
    localStorage.removeItem('newLocation')
    this.router.navigate(['/signup']);
  }
  
}
