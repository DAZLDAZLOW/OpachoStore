using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpachoStore.Models;

namespace OpachoStore.Pages
{
    public class CompletedModel : PageModel
    {
        public Order Order;
        public void OnGet([FromServices]IStoreRepository repository)
        {
            Guid id; 
                bool result = Guid.TryParse(this.Request.Query["OrderCode"],out id);
            if (result)
                Order = repository.Orders.FirstOrDefault(x => x.OrderCode == id) ?? new Order();
            else
                Order = new();
        }
    }
}
