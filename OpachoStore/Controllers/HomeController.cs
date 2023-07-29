using Microsoft.AspNetCore.Mvc;
using OpachoStore.Models;
using OpachoStore.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace OpachoStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStoreRepository Repository;

        public HomeController(ILogger<HomeController> logger, IStoreRepository storeRepository)
        {
            _logger = logger;
            Repository = storeRepository;
        }

        public IActionResult Index()
        {
            MainPageViewModel mpvm = new MainPageViewModel()
            {
                HighlightedCategories = new[]
                {
                    new PopularCategoryItem() { Name = "Процессоры", ImageUrl = "Процессоры.png" },
                    new PopularCategoryItem() { Name = "Видеокарты", ImageUrl = "Видеокарты.png" },
                    new PopularCategoryItem() { Name = "Оперативная память", ImageUrl = "Оперативная память.png" },
                },
                HighlightedProducts = Repository.Products.Take(8).ToArray()
            };

            return View(mpvm);
        }

        public IActionResult Privacy()
        {
            ContactsInfo contactsInfo = Repository.ContactsInfos.FirstOrDefault() ?? new()
            {
                Email = "Не задан",
                Phone = "Не задан"
            };
            ViewBag.Email = contactsInfo.Email;
            ViewBag.Phone = contactsInfo.Phone;
            return View();
        }

        public IActionResult Track(Guid? OrderCode)
        {
            if (OrderCode == null)
            {
                ViewBag.NotFound = false;
                return View();
            }
            var order = Repository.Orders.FirstOrDefault(x => x.OrderCode == OrderCode);
            if (order == null)
            {
                ViewBag.NotFound = true;
                return View();
            }
            else return View(order);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}