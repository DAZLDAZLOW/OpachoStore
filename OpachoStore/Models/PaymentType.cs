using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OpachoStore.Models
{
    public class PaymentType
    {
        [BindNever]
        public int PaymentTypeId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
