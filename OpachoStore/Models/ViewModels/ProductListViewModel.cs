namespace OpachoStore.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set;} = Enumerable.Empty<Product>();
        public PagingInfo PagingInfo { get; set;} = new PagingInfo();
        public string? CurrentCategory { get; set; }
        public int MaxPrice { get; set; }
        public int MinPrice { get; set;}
        public int CurrentMinPrice { get; set; }
        public int CurrentMaxPrice { get; set;}
        public string CurrentSortName { get; set; } = "";
    }
}
