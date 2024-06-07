using Lamon.Data;
using Lamon.Models;

namespace Lamon.Services
{
    public class OrderService : IServiceBase<Order>
    {
        private readonly ApplicationDbContext context;

        public OrderService(ApplicationDbContext context)
        {
            this.context = context;
        }
        public int Add(Order Model)
        {
            Model.CreatedDate = DateTime.Now;
            Model.DeliveryDate = DateTime.Parse(System.DateTime.Now.AddDays(5).ToString("yyyy-MM-dd"));
            Model.StatusOfOrder = StatusOfOrder.PendingStatus;
            context.Orders.Add(Model);
            context.SaveChanges();
            return Model.Id;
        }

        public int Delete(int id)
        {
            context.Orders.Remove(context.Orders.FirstOrDefault(s => s.Id == id));
            context.SaveChanges();
            return 1;
        }

        public IEnumerable<Order> Get()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            return context.Orders.ToList();
        }

        public Order GetDetails(int id)
        {
            return context.Orders.FirstOrDefault(o => o.Id == id);
        }

        public Order Search(string name)
        {
            throw new NotImplementedException();
        }

        public int Update(int id, Order Model)
        {
            Order order = GetDetails(id);
            order.StatusOfOrder = Model.StatusOfOrder;

            context.Orders.Update(order);
            context.SaveChanges();
            return id;
        }
    }
}
