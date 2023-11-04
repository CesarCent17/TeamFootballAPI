namespace TeamFootballAPI.Models
{
    public class Team
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int YearFounded { get; set; }
    }
}
