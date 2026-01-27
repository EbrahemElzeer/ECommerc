using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domin.Model.OrderModule
{
    public class Order:BaseEntity<Guid>
    {
        public string UserEmail { get; set; } = default!;
        public int DeliveryMethodId { get; set; }
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.Now;
        public OrderStatus status {  get; set; }= OrderStatus.Pending;
        public OrderAdderess Address { get; set; }= default!;
        public DeliveryMethod DeliveryMethod { get; set; }= default!;
        public ICollection<OrderItem> OrderItems { get; set; }= [];
        public decimal Subtotal { get; set; }
        public decimal GetTotal () => Subtotal + DeliveryMethod.Price;
    }
}
