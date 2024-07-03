namespace OnlineMovieTicketingApplication.Models
{
    public class JWTsettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireDays { get; set; }
    }
}
