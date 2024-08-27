import { Component,OnInit,ViewChild,ElementRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-admindashboard',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './admindashboard.component.html',
  styleUrl: './admindashboard.component.css'
})
export class AdmindashboardComponent implements OnInit{
  authToken: string |null='';
  moviesData: any[] = [];
  showsData: any[] = [];
  theatresData: any[] = [];
  newMovie:any={};
  newShow: any = {};
  newTheatre: any = {};
  editMovie: any = null;
  showAddMovieForm: boolean = false;
  showAddTheatreForm: boolean = false;
  showAddShowForm: boolean = false;
  showEditMovieForm: boolean = false;
  activeTab: string = 'movies-section';

  @ViewChild('editMovieForm', { static: false }) editMovieForm!: ElementRef;
  constructor(private http: HttpClient, private router: Router) {
    this.authToken = localStorage.getItem('authToken');
  }
  ngOnInit(): void {
    this.fetchMovies();
    this.fetchTheatres();
    this.fetchShows();
  }
  openTab(tabName: string): void {
    this.activeTab = tabName;
  }

  fetchMovies(): void {
    this.http.get<any>('https://localhost:7039/api/movies/movies', {
      headers: {
        'Authorization': `Bearer ${this.authToken}`
      }
    }).subscribe((data: any[]) => {
      this.moviesData = data;
    }, error => {
      console.error('Error fetching movies:', error);
    });
  }
  
  fetchTheatres(): void {
    this.http.get<any>('https://localhost:7039/api/theatre', {
      headers: {
        'Authorization': `Bearer ${this.authToken}`
      }
    }).subscribe((data: any[]) => {
      this.theatresData = data;
    }, error => {
      console.error('Error fetching theatres:', error);
    });
  }
  fetchShows(): void {
    this.http.get<any>('https://localhost:7039/api/shows', {
      headers: {
        'Authorization': `Bearer ${this.authToken}`
      }
    }).subscribe((data: any[]) => {
      this.showsData = data;
    }, error => {
      console.error('Error fetching shows:', error);
    });
  }
  logout(): void {
    localStorage.removeItem('authToken');
    this.router.navigate(['/admin']);
  }
  cancelAddMovie(): void {
    this.newMovie = {}; 
    this.showAddMovieForm = false; 
  }
  cancelTheatre():void{
    this.newTheatre = {};
    this.showAddTheatreForm = false;
  }

  deleteMovie(movieId: number): void {
    if (confirm('Are you sure you want to delete this movie?')) {
      this.http.delete(`https://localhost:7039/api/movies/deletemovie/${movieId}`, {
        headers: {
          'Authorization': `Bearer ${this.authToken}`
        }
      }).subscribe(() => {
        this.fetchMovies();
      }, error => {
        console.error('Error deleting movie:', error);
      });
    }
  }
  deleteTheatre(theatreId: number): void {
    if (confirm('Are you sure you want to delete this theatre?')) {
      this.http.delete(`https://localhost:7039/api/theatre/deletetheatre/${theatreId}`, {
        headers: {
          'Authorization': `Bearer ${this.authToken}`
        }
      }).subscribe(() => {
        this.fetchTheatres();
      }, error => {
        console.error('Error deleting theatre:', error);
      });
    }
  }
  deleteShow(showId: number): void {
    if (confirm('Are you sure you want to delete this show?')) {
      this.http.delete(`https://localhost:7039/api/shows/deleteshow/${showId}`, {
        headers: {
          'Authorization': `Bearer ${this.authToken}`
        }
      }).subscribe(() => {
        this.fetchShows();
      }, error => {
        console.error('Error deleting show:', error);
      });
    }
  }
  addMovie(): void {
    this.http.post('https://localhost:7039/api/Movies', this.newMovie, {
      headers: {
        'Authorization': `Bearer ${this.authToken}`
      }
    }).subscribe(() => {
      this.fetchMovies();
      this.showAddMovieForm = false;
    }, error => {
      console.error('Error adding movie:', error);
    });
  }
  addShow(): void {
    this.http.post('https://localhost:7039/api/shows/addshow', this.newShow, {
      headers: {
        'Authorization': `Bearer ${this.authToken}`
      }
    }).subscribe(() => {
      this.fetchShows();
      this.showAddShowForm = false;
    }, error => {
      console.error('Error adding show:', error);
    });
  }
  addTheatre(): void {
    this.http.post('https://localhost:7039/api/theatre/addtheatre', this.newTheatre, {
      headers: {
        'Authorization': `Bearer ${this.authToken}`
      }
    }).subscribe(() => {
      this.fetchTheatres();
      this.showAddTheatreForm = false;
    }, error => {
      console.error('Error adding theatre:', error);
    });
  }
  convertToDateFormat(dateString: string): string {
    if (!dateString) return '';
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = ('0' + (date.getMonth() + 1)).slice(-2); // Months are zero-indexed
    const day = ('0' + date.getDate()).slice(-2);
    return `${year}-${month}-${day}`;
  }
  
  editMovieMethod(movieId: number): void {
    this.http.get(`https://localhost:7039/api/movies/${movieId}`, {
      headers: {
        'Authorization': `Bearer ${this.authToken}`
      }
    }).subscribe((movie: any) => {
      this.editMovie = {
        ...movie,
        releaseDate: this.convertToDateFormat(movie.releaseDate)
      };
      this.showEditMovieForm = true; 
      setTimeout(() => {
        this.editMovieForm?.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'start' });
      }, 100);
    }, error => {
      console.error('Error fetching movie details for edit:', error);
    });
  }

  updateMovie(): void {
    this.http.put(`https://localhost:7039/api/movies/editmovie/${this.editMovie.id}`, this.editMovie, {
      headers: {
        'Authorization': `Bearer ${this.authToken}`
      }
    }).subscribe(() => {
      this.fetchMovies();
      this.editMovie = null; 
      this.showEditMovieForm=false;
    }, error => {
      console.error('Error updating movie:', error);
    });
  }
  cancelEdit(): void {
    this.editMovie = null;
    this.showEditMovieForm = false; 
  }
}
