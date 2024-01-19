using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameModel.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int VideoGameId { get; set; }
        public VideoGame VideoGame { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
