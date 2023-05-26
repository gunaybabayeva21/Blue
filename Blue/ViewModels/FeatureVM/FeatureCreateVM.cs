

namespace Blue.ViewModels.FeatureVM
{
    public class FeatureCreateVM
    {
        public string Title { get; set; } = null!;
        public string Description { get;set; }= null!;
        public IFormFile Image { get; set; } = null!;
    }
}
