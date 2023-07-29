using Microsoft.EntityFrameworkCore;

namespace OpachoStore.Models
{
    public class OpachoStoreContext : DbContext
    {
        public OpachoStoreContext(DbContextOptions<OpachoStoreContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<PaymentType> PaymentTypes => Set<PaymentType>();
        public DbSet<OrderStatus> OrderStatuses => Set<OrderStatus>();
        public DbSet<ContactsInfo> ContactsInfo => Set<ContactsInfo>();
        public DbSet<Property> Property => Set<Property>();
        public DbSet<Specification> Specification => Set<Specification>();
    }
}
