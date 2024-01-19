namespace LibraryModel.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int BookId {  get; set; }
        public Book Book { get; set; }
        public DateTime OrderDate { get; set; } 
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
