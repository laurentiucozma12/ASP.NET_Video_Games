namespace VideoGameModel.Models
{
    public class Studio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<VideoGame>? VideoGames { get; set; }
    }
}
