namespace LibraryModel.Models
{
    public class PublishedBook
    {
        public int PublisherId { get; set; }
        public Publisher? Publisher { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}
