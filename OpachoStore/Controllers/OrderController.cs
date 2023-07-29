using Microsoft.AspNetCore.Mvc;
using OpachoStore.Models;

namespace OpachoStore.Controllers
{
    public class OrderController : Controller
    {
        private IStoreRepository repo;
        private Cart cart;
        public OrderController(IStoreRepository repository, Cart cart)
        {
            repo = repository;
            this.cart = cart;
        }
        public IActionResult Checkout()
        {
            ViewBag.PaymentTypes = repo.PaymentTypes.ToArray();

            return View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order, string paymentTypeId)
        {
            if(cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Ваша корзина пуста!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                order.OrderCode = Guid.NewGuid();
                order.PaymentType = repo.PaymentTypes.FirstOrDefault(x => x.PaymentTypeId == Convert.ToInt32(paymentTypeId));
                order.Status = repo.OrderStatuses.FirstOrDefault(x => x.OrderStatusId == 1)!; //Присвоение статуса созданному заказу
                order.CreatedAt = DateTime.Now;
                repo.SaveOrder(order);
                cart.Clear();
                return RedirectToPage("/Completed", new { orderCode = order.OrderCode });
            }
            ViewBag.PaymentTypes = repo.PaymentTypes.ToArray();
            return View(order);
        }
    }
}
