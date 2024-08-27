import { Component,OnInit} from '@angular/core';
import {Router} from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-ticket',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ticket.component.html',
  styleUrl: './ticket.component.css'
})
export class TicketComponent implements OnInit {
  ticketInfo:any;
  formattedDate?:string;
  formattedTime?:string;

  constructor(private router: Router) { }
  ngOnInit(): void {
    const ticketInfo = localStorage.getItem('ticketInfo');
    if (ticketInfo) {
      this.ticketInfo = JSON.parse(ticketInfo);
      const dateTime = new Date(this.ticketInfo.showTime);
      this.formattedDate = dateTime.toLocaleDateString('en-GB', { day: 'numeric', month: 'long', year: 'numeric' });
      this.formattedTime = dateTime.toLocaleTimeString('en-US', { hour: 'numeric', minute: '2-digit', hour12: true });
    } else {
      this.router.navigate(['/dashboard']);
    }
  }

  returnToDashboard(): void {
    this.router.navigate(['/dashboard']);
  }

}
