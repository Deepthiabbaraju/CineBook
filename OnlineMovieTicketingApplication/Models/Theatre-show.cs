namespace OnlineMovieTicketingApplication.Models
{
    public class Theatre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
    public class Shows
    {
        public int Id { get; set; }
        public int TheatreId { get; set; }
        public int MovieId { get; set; }
        public DateTime ShowTime { get; set; }
        public int AvailableSeats { get; set; }
        
    }
    public class Bookings
    {
        public int Id { get; set; }
        public int ShowId { get; set; }
        public string SeatNumber { get; set; }
        
    }

}
