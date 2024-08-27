import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-seatselection',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './seatselection.component.html',
  styleUrl: './seatselection.component.css'
})
export class SeatselectionComponent {
  movieTitle?: string;
  seats?: any[][]=[];
  selectedSeats: any[] = [];
  ticketInfo: any;
  theatreName: string='';
  j?:number;
  theatreId?: number;
  showSeatSelectionModal = true;
  numberOfSeats = 1;
  maxSelectableSeats = 0;
  currentSelection: any[] = [];

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
    const ticketInfo = JSON.parse(localStorage.getItem('ticketInfo') ?? '{}');
    this.movieTitle = ticketInfo.movieTitle;
    this.theatreName = ticketInfo.theatreName;
    this.ticketInfo = ticketInfo;
    this.fetchTheatreIdByName(this.theatreName).subscribe((response:any) => {
      const theatreId = response;
      this.theatreId = theatreId;
      this.fetchBookedSeats(theatreId, this.ticketInfo.showTime).subscribe((bookedSeats: any[]) => {
        console.log('Booked seats response:', bookedSeats);
        this.seats = this.generateSeats(bookedSeats);
      });
    });
  }

  fetchTheatreIdByName(theatreName: string): Observable<any[]>{
    return this.http.get<any[]>(`https://localhost:7039/api/Theatre/GetTheatreIdByName?theatreName=${encodeURIComponent(theatreName)}`);
  }

  fetchBookedSeats(theatreId: number, showTime: string): Observable<any[]> {
    return this.http.get<any[]>(`https://localhost:7039/api/Seats/getBookedSeats?theatreId=${theatreId}&showTime=${showTime}`);
  }

  generateSeats(bookedSeats: any[]) {
    const seats = [];
    for (let i = 0; i < 6; i++) {
      const row = [];
      for (let j = 0; j < 16; j++) {
        const seatId = `${String.fromCharCode(65 + i)}${j + 1}`;
        const status = bookedSeats.includes(seatId) ? 'reserved' : 'available';
        row.push({ id: seatId, status });
      }
      seats.push(row);
    }
    return seats;
  }
  confirmSelection() {
    this.showSeatSelectionModal = false;
    this.maxSelectableSeats = this.findMaxContiguousSeats();
  }

  toggleSeatSelection(seat: any) {
    if (seat.status !== 'available') return;
    if (this.selectedSeats.length === this.numberOfSeats) {
      this.deselectSeats();
    }

    const contiguousSeats = this.getContiguousSeats(seat);

    if (this.currentSelection.length > 0) {
      if (contiguousSeats.length + this.currentSelection.length > this.numberOfSeats) {
        alert('Cannot select more seats. Please deselect some seats before selecting more.');
        return;
      }
    }

    //this.currentSelection.push(...contiguousSeats);
    contiguousSeats.forEach((s: any) =>{ s.status = 'selected';this.selectedSeats.push(s);});
    //this.selectedSeats = [...this.currentSelection];

    if (this.selectedSeats.length >= this.numberOfSeats) {
      this.currentSelection = [];
    }
  }

  getContiguousSeats(startSeat: any) {
    const startIndex = this.getSeatIndex(startSeat);
    if (startIndex === -1) return [];

    const contiguousSeats = [];
    let seatsToSelect = this.numberOfSeats - this.selectedSeats.length;

    for (let i = startIndex; i < startIndex + seatsToSelect; i++) {
      const seat = this.getSeatAtIndex(i);
      if (seat && seat.status === 'available') {
        contiguousSeats.push(seat);
      } else {
        break;
      }
    }
    console.log('Selected seats:', this.selectedSeats);
    return contiguousSeats;
  }

  deselectSeats() {
    this.selectedSeats.forEach((seat) => {
      if (seat) seat.status = 'available';
    });
    this.selectedSeats = [];
    this.currentSelection = [];
  }

  findMaxContiguousSeats(): number {
    let maxSeats = 0;
    for (const row of this.seats ?? []) {
      let contiguousSeats = 0;
      for (const seat of row) {
        if (seat.status === 'available') {
          contiguousSeats++;
        } else {
          maxSeats = Math.max(maxSeats, contiguousSeats);
          contiguousSeats = 0;
        }
      }
      maxSeats = Math.max(maxSeats, contiguousSeats);
    }
    return maxSeats;
  }

  getSeatIndex(seat: any): number {
    if (!this.seats) return -1;
    for (let i = 0; i < (this.seats?.length ?? 0); i++) {
      const index = this.seats[i].indexOf(seat);
      if (index !== -1) return i * 16 + index;
    }
    return -1;
  }

  getSeatAtIndex(index: number): any {
    if (!this.seats) return null;
    const rowIndex = Math.floor(index / 16);
    const seatIndex = index % 16;
    return this.seats[rowIndex]?.[seatIndex] ?? null;
  }
  
  proceed() {
    const formattedSeats = this.selectedSeats.map(seat => seat.id.replace('seat-', ''));
    const updateSeatsRequest = {
      seatIds: formattedSeats,
      status: false,
      movieId: this.ticketInfo.movieId,
      theatreId: this.theatreId,
      showTime: this.ticketInfo.showTime
    };
    console.log('Selected seats:', formattedSeats);

    this.http.post('https://localhost:7039/api/Seats/updateSeats', updateSeatsRequest).subscribe((response: any) => {
      if (response.success) {
        localStorage.setItem('ticketInfo', JSON.stringify({ ...this.ticketInfo, seats: formattedSeats }));
        this.router.navigate(['/ticket']);
      } else {
        alert('Failed to update seats. Please try again.');
      }
    }, (error: any) => {
      console.error('Error:', error);
      alert('An error occurred. Please try again.');
    });
  }


}
