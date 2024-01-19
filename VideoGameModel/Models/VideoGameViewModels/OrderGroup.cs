using System.ComponentModel.DataAnnotations;

namespace VideoGameModel.Models.VideoGameViewModels
{
    public class OrderGroup
    {
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }
        public int VideoGameCount { get; set; }
    }
}
