using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpachoStore.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }
        [BindNever]
        [Required(ErrorMessage = "Код заказа указан некоректно")]
        public Guid OrderCode { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();

        [Required(ErrorMessage = "Укажите номер телефона, чтобы курьер мог с вами свзязаться")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Укажите Ф.И.О. получателя")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Укажите адрес доставки")]
        public string? Adress { get; set; }

        [BindNever]
        public PaymentType PaymentType { get; set; } = new ();

        [BindNever]
        public OrderStatus Status { get; set; } = new();

        [BindNever]
        public DateTime? CreatedAt { get; set; } 

        public string GetDateString()
        {
            if(CreatedAt == null) return string.Empty;
            return ((DateTime)CreatedAt).ToString("dd.MM.yyyy HH:mm");
        }
    }
}
