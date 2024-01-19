namespace VideoGameModel.Models.VideoGameViewModels
{
    public class StudioIndexData
    {
        public IEnumerable<VideoGame> VideoGames { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
