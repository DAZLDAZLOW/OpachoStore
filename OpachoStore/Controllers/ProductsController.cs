using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpachoStore.Models;
using OpachoStore.Models.ViewModels;
using OpachoStore.Pages.Admin;
using System.Collections.Generic;
using iText.Pdfa;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using System.Diagnostics;

namespace OpachoStore.Controllers
{
    public class ProductsController : Controller
    {
        public const string SessionKeyPageSize = "_PageSize";
        public int DefaultPageSize = 5;
        private IStoreRepository repo;

        public ProductsController(IStoreRepository repos)
        {
            repo = repos;
        }

        public IActionResult Index(string? category, int? pageSize, int productPage = 1, int priceStarts = 0, int priceEnds = 0, string sortName = "")
        {
            if (HttpContext.Request.Headers["User-Agent"].Any(x => x.ToLower().Contains("mobi")))
                ViewBag.IsMobile = true;
            else
                ViewBag.IsMobile = false;
            if (HttpContext.Session.GetInt32(SessionKeyPageSize) == null || pageSize != null)
                HttpContext.Session.SetInt32(SessionKeyPageSize,pageSize ?? DefaultPageSize);
            int PageSize = HttpContext.Session.GetInt32(SessionKeyPageSize)!.Value;
            var query = repo.Products.Where(p => category == null || p.Category == category);
            var minPrice = query.Any()?query.Min(p => p.Price): 0;
            var maxPrice = query.Any()?query.Max(p => p.Price): 0;
            if (priceStarts > 0)
                query = query.Where(p => p.Price >= priceStarts);
            if(priceEnds > 0 && priceEnds > priceStarts)
                query = query.Where(p => p.Price <= priceEnds);
            query = SortBy(query, sortName)!;
            var totalItems = query.Count();
            query = query.Skip((productPage - 1) * PageSize);
            var products = query.Take(PageSize);

            return View(new ProductListViewModel
            {
                Products = products,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize!,
                    TotalItems = totalItems
                },
                CurrentCategory= category,
                MinPrice= Convert.ToInt32(minPrice),
                MaxPrice= Convert.ToInt32(maxPrice),
                CurrentMinPrice = Convert.ToInt32(priceStarts) ,
                CurrentMaxPrice = priceEnds != 0? Convert.ToInt32(priceEnds): Convert.ToInt32(maxPrice),
                CurrentSortName = sortName.ToLower(),
            });
            
        }

        public IActionResult Product(int productId)
        {
            Product p = repo.Products.Include(x => x.Properties).ThenInclude(e => e.Specification).FirstOrDefault(x => x.ProductID == productId);
            if(p == null) return NotFound(); 
            return View(p);
        }

        [HttpGet("SearchProducts")]
        public IActionResult SearchProducts(string searchQuery,int? pageSize, int productPage = 1)
        {
            if (HttpContext.Session.GetInt32(SessionKeyPageSize) == null || pageSize != null)
                HttpContext.Session.SetInt32(SessionKeyPageSize, pageSize ?? DefaultPageSize);
            int PageSize = HttpContext.Session.GetInt32(SessionKeyPageSize)!.Value;

            PageSize = pageSize ?? PageSize;
            ViewBag.SearchRequest = searchQuery;

            if (string.IsNullOrEmpty(searchQuery))
            {
                return View(new ProductListViewModel
                {
                    Products = Array.Empty<Product>(),
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = productPage,
                        ItemsPerPage = PageSize,
                    }
                });
            }

            ProductListViewModel plvm = new ProductListViewModel
            {
                Products = repo.Products.Where(p => p.Title.ToLower().Contains(searchQuery.ToLower())).OrderBy(p => p.ProductID).Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,

                }
            };
            
            plvm.PagingInfo.TotalItems = plvm.Products.Count();
            return View(plvm); 
        }

        [HttpGet("pricelist")]
        public async Task<IActionResult> GetPriceList()
        {
            var products = repo.Products.OrderBy(x => x.Category).ToArray();
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            PdfFont f = PdfFontFactory.CreateFont("./wwwroot/fonts/timesnewromanpsmt.ttf", "Identity-H");

            Paragraph header= new Paragraph("Прайс-лист на " + DateTime.Now.ToString("yyyy.MM.dd")).SetFont(f);
            document.Add(header);
            Table table = new Table(4);
            table.AddCell(new Cell().Add(new Paragraph("ID").SetFont(f)));
            table.AddCell(new Cell().Add(new Paragraph("Наименование").SetFont(f)));
            table.AddCell(new Cell().Add(new Paragraph("Категория").SetFont(f)));
            table.AddCell(new Cell().Add(new Paragraph("Стоимость, руб.").SetFont(f)));
            foreach (var product in products)
            {
                table.AddCell(new Cell().Add(new Paragraph(product.ProductID.ToString()).SetFont(f)));
                table.AddCell(new Cell().Add(new Paragraph(product.Title).SetFont(f)));
                table.AddCell(new Cell().Add(new Paragraph(product.Category).SetFont(f)));
                table.AddCell(new Cell().Add(new Paragraph(product.GetStringPrice(true)).SetFont(f)));
            }
            document.Add(table);
            document.Close();

            byte[] bytes = stream.ToArray();

            return File(bytes, "application/pdf");
        }

        private IQueryable<Product?> SortBy(IQueryable<Product?> query,string sortName)
        {
            if(!query.Any()) return query;
            switch (sortName.ToLower())
            {
                case "pricedec":
                    return query.OrderByDescending(p => p.Price);
                case "priceinc":
                    return query.OrderBy(p => p.Price);
                case "titledec":
                    return query.OrderByDescending(p => p.Title);
                case "titleinc":
                    return query.OrderBy(p => p.Title);
                default:
                    return query.OrderBy(p => p.Title);
            }

        }
    }
}
