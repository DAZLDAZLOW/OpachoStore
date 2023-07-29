using Microsoft.EntityFrameworkCore;
using OpachoStore.Pages.Admin;

namespace OpachoStore.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private OpachoStoreContext context;
        public EFStoreRepository(OpachoStoreContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Product> Products => context.Products;

        public IQueryable<Order> Orders => context.Orders.Include(s => s.Status).Include(pt => pt.PaymentType).Include(o => o.Lines).ThenInclude(l => l.Product);

        public IQueryable<PaymentType> PaymentTypes => context.PaymentTypes;

        public IQueryable<OrderStatus> OrderStatuses => context.OrderStatuses;

        public IQueryable<ContactsInfo> ContactsInfos => context.ContactsInfo;

        public IQueryable<Property> Properties => context.Property;

        public IQueryable<Specification> Specifications => context.Specification;


        public void CreateSpecification(Specification specification)
        {
            context.Add(specification);
            context.SaveChanges();
        }

        public void SaveContacts(ContactsInfo contactsInfo)
        {
            context.SaveChanges();
        }

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product));
            if(order.OrderId == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }

        public void CreateProduct(Product p)
        {
            context.Add(p);
            context.SaveChanges();
        }
        public void DeleteProduct(Product p)
        {
            context.Remove(p);
            context.SaveChanges();
        }
        public void SaveProduct(Product p)
        {
            context.SaveChanges();
        }

        public void CreateContacts(ContactsInfo contactsInfo)
        {
            context.Add(contactsInfo);
            context.SaveChanges();
        }

        public void CreateProperty(Property property)
        {
            context.Add(property);
            context.SaveChanges();
        }
    }
}
