namespace Asteroids_App.Models
{
    public class Neo
    {
        public string Name { get; set; }
        public double EstimatedDiameterMin { get; set; }
        public double EstimatedDiameterMax { get; set; }
        public bool IsPotentiallyHazardous { get; set; }
        public string CloseApproachDate { get; set; }
        public double MissDistanceKm { get; set; }
    }
}
