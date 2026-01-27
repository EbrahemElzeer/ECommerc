using ECommerce.Domin.Model.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Service.Specifications.OrderSpecifications
{
    public class OrderSpecification : BaseSpecifications<Order, Guid>
    {
        public OrderSpecification(string email) : base(o=>o.UserEmail==email)
        {
            
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.OrderItems);
            AddOrderByDescending(o => o.OrderDate);
        }
        public OrderSpecification(string email, Guid Id) : base(o => o.Id == Id && o.UserEmail==email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
            
            
        }
    }
}
