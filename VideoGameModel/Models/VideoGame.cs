using System.ComponentModel.DataAnnotations.Schema;

namespace VideoGameModel.Models
{
    public class VideoGame
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<PublishedVideoGame>? PublishedVideoGames { get; set; }

        [ForeignKey("StudioId")]
        public int? StudioId { get; set; }
        public Studio? Studio { get; set; }
        public ICollection<Genre>? Genres { get; set; }
        public ICollection<Platform>? Platforms { get; set; }
    }
}
