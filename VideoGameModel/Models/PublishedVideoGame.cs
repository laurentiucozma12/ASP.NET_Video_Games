using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameModel.Models
{
    public class PublishedVideoGame
    {
        public int StudioId { get; set; }
        public Studio? Studio { get; set; }
        public int VideoGameId { get; set; }
        public VideoGame? VideoGame { get; set; }
    }
}
