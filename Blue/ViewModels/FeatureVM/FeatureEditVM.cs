namespace Blue.ViewModels.FeatureVM
{
    public class FeatureEditVM
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImageName { get; set; }
        public IFormFile? Image { get; set; } 
    }
}
