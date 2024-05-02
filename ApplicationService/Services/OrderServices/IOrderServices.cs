using ApplicationService.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.OrderServices
{
    public interface IOrderServices
    {
        Task<int> CreateAsync(OrderViewModel orderViewModel);
        void Delete(Order order);
        Task<Order> GetOrderAsync(int id);
        void Update(Order order);
    }
}
