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

        [ForeignKey("GenreId")]
        public int? GenreId { get; set; }
        public Genre? Genre { get; set; }

        [ForeignKey("PlatformId")]
        public int? PlatformId { get; set; }
        public Platform? Platform { get; set; }
    }
}
