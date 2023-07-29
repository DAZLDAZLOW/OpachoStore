namespace OpachoStore.Models
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }

        void SaveProduct(Product p);
        void CreateProduct(Product p);
        void DeleteProduct(Product p);

        IQueryable<Order> Orders { get; }
        IQueryable<PaymentType> PaymentTypes { get; }
        IQueryable<OrderStatus> OrderStatuses { get; }

        void SaveOrder(Order order);

        IQueryable<ContactsInfo> ContactsInfos { get; }

        void SaveContacts(ContactsInfo contactsInfo);
        void CreateContacts(ContactsInfo contactsInfo);

        public IQueryable<Property> Properties { get; }

        public IQueryable<Specification> Specifications { get; }


        public void CreateSpecification(Specification specification);

        public void CreateProperty(Property property);
    }
}
