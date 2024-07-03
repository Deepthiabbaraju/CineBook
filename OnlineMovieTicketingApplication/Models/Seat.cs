using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineMovieTicketingApplication.Models
{
    public class Seat
    {

        [Key, Column(Order = 1)]
        public int TheatreId { get; set; }

        [Key, Column(Order = 2)]
        public DateTime ShowTime { get; set; }

        [Key, Column(Order = 3)]
        public string Id { get; set; } 

        public bool Available { get; set; }
        public int MovieId { get; set; }

        public Movie Movie { get; set; }
        public Theatre Theatre { get; set; }
    }
}
