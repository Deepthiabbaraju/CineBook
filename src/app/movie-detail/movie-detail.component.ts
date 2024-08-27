import { Component,OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Movie } from '../dashboard/dashboard.component';
import { ActivatedRoute ,Router} from '@angular/router';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';


@Component({
  selector: 'app-movie-detail',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './movie-detail.component.html',
  styleUrl: './movie-detail.component.css'
})
export class MovieDetailComponent implements OnInit{
  movie:any;
  movieId?: number ;
  moviePoster: string='';
  movieTitle: string='';
  genre: string='';
  language: string='';
  duration: string='';
  releaseDate?: Date;

  constructor(private http: HttpClient,private route:ActivatedRoute,private router:Router) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.movieId = params['id'];
      console.log(this.movieId);
      if (this.movieId) {
        this.fetchMovieDetails();
      } else {
        console.error('No movie ID found in URL');
      }
    });
  }

  fetchMovieDetails(): void {
    this.http.get(`https://localhost:7039/api/movies/${this.movieId}`)
      .subscribe(response => {
        this.movie = response;
        this.moviePoster = this.movie.imageUrl;
        this.movieTitle = this.movie.title;
        this.genre = this.movie.genre;
        this.language = this.movie.language;
        this.duration = this.movie.duration;
        this.releaseDate = this.movie.releaseDate;
      }, error => {
        console.error('Error fetching movie details:', error);
      });
  }

  showTheatres(): void {
    if(this.movieId){
    this.router.navigate(['/booking', this.movieId]);
    }
  }

  

}
