import { Component,OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Movie} from '../dashboard/dashboard.component';
import { ActivatedRoute , Router} from '@angular/router';
import {CommonModule} from '@angular/common';
import { RouterModule } from '@angular/router';
interface Theatre {
  theatreName: string;
  showTimes:Showtime[];
  
}
interface Showtime {
  showTime: Date;
}
@Component({
  selector: 'app-booking',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent implements OnInit{
  movie?: Movie;
  movieTitle?:string;
  dates: Date[] = [];
  theatres: Theatre[] = [];
  selectedDate: Date;
  movieId?: number;


  constructor(private http: HttpClient, private route: ActivatedRoute, private router: Router) {this.selectedDate = new Date(); }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.movieId = +params['movieId'];
      console.log('Received movieId:', this.movieId);

      // Define default dates
      const today = new Date();
      const tomorrow = new Date(today);
      tomorrow.setDate(today.getDate() + 1);
      const dayAfterTomorrow = new Date(today);
      dayAfterTomorrow.setDate(today.getDate() + 2);

      this.dates = [today, tomorrow, dayAfterTomorrow];
      this.selectedDate = this.dates[0]; 

      
      this.fetchMovieDetails();
    });
  }
  fetchMovieDetails(): void {
    if (this.movieId === undefined) return;

    this.http.get(`https://localhost:7039/api/movies/${this.movieId}`)
      .subscribe((response: any) => {
        this.movieTitle = response.title;
        this.loadTheatresForDate(this.selectedDate);
      });
  }
  loadTheatresForDate(date: Date): void {
    const isoDate = date.toISOString().split('T')[0]; // Get YYYY-MM-DD format
    this.http.get<Theatre[]>(`https://localhost:7039/api/movies/${this.movieId}/theatres?date=${isoDate}`)
      .subscribe((response: Theatre[]) => {
        this.theatres = response;
      });
  }

  selectDate(date: Date): void {
    this.selectedDate = date;
    this.loadTheatresForDate(date);
  }

  selectShowtime(theatre: Theatre, showtime: Showtime): void {
    const ticketInfo = {
      movieId: this.movieId,
      movieTitle: this.movieTitle,
      theatreName: theatre.theatreName,
      showTime: showtime.showTime
    };
    localStorage.setItem('ticketInfo', JSON.stringify(ticketInfo));
    console.log(ticketInfo);
    this.router.navigate(['/seatselection']);
  }

  formatTime(dateTimeString:Date| string): string {
    const date = new Date(dateTimeString);
    let hours:number|string = date.getHours();
    let minutes:number|string = date.getMinutes();
    const ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12;
    minutes = minutes < 10 ? '0' + minutes : minutes;
    return `${hours}:${minutes} ${ampm}`;
  }
  formatDate(date: Date): string {
    const year = date.getFullYear();
    let month: number | string = date.getMonth() + 1;
    let day: number | string = date.getDate();
    if (month < 10) month = `0${month}`;
    if (day < 10) day = `0${day}`;
    return `${year}-${month}-${day}`;
  }


}
