namespace OpachoStore.Models.ViewModels
{
    public class MainPageViewModel
    {
        public IEnumerable<PopularCategoryItem> HighlightedCategories = new List<PopularCategoryItem>();
        public IEnumerable<Product> HighlightedProducts = new List<Product>();
    }

    public class PopularCategoryItem
    {
        public string Name { get; set; } = "";
        public string ImageUrl { get; set; } = "";
    }
}
