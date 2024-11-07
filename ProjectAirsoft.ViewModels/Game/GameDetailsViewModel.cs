namespace ProjectAirsoft.ViewModels.Game
{
    public class GameDetailsViewModel
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string Date { get; set; } = null!;

        public string StartTime { get; set; } = null!;

        public int RegisteredPlayers { get; set; }

        public int Capacity { get; set; }

        public decimal Fee { get; set; }

        public string Terrain { get; set; } = null!;

        public string Organizer { get; set; } = null!;
    }
}
