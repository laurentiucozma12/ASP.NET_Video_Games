using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameModel.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("VideoGameId")]
        public int? VideoGameId { get; set; }
        public VideoGame? VideoGame { get; set; }
    }
}
