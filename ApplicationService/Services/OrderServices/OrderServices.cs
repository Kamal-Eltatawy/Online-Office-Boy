using ApplicationService.Services.UserServices;
using ApplicationService.ServicesHelper;
using ApplicationService.ViewModels;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Reposatory;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly IUserServices userServices;
        private readonly IRepository<Order> repository;
        public OrderServices(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper,IUserServices userServices)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.userServices = userServices;
            this.repository = unitOfWork.GetRepository<Order>();
        }

        public async Task<int> CreateAsync(OrderViewModel orderViewModel)
        {
            var currentOfficeBoy = userServices.GetOfficeBoyWithCurrentShiftAsync().Result.FirstOrDefault();

            if (currentOfficeBoy is null)
            {
                return -5;
            }
            var currentUserOfficeId =  userServices.GetCurrentUserIncludingOfficeAsync().Result.OfficeId;
            Order order = new Order()
            {
                CreatedDate = DateTime.Now.Date,
                UserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            };
            var orderProducts = mapper.Map<List<OrderProducts>>(orderViewModel.OrderProductsData);

            foreach (var item in orderProducts)
            {
                if(string.IsNullOrEmpty(item.OfficeBoyId)||item.OfficeBoyId=="0")
                {
                    item.OfficeBoyId = currentOfficeBoy.Id;
                }
                if (item.DestiniationDepartmentId == 0)
                {
                    item.DestiniationDepartmentId = currentUserOfficeId;
                }
                item.OrderId = order.Id;
                order.OrderProducts.Add(item);

            }
            await repository.AddAsync(order);

            return await unitOfWork.SaveChangesAsync();

        }


        public async Task<Order> GetOrderAsync(int id)
        {

            return await repository.GetByIdAsync(id);
        }

        public  void Update(Order order)
        {

              repository.Update(order);
        }

        public  void Delete(Order  order)
        {

           repository.Delete(order);
        }



    }
}
