using ApplicationService.Services.OrderServices;
using ApplicationService.Services.UserServices;
using ApplicationService.ViewModels;
using AutoMapper;
using Domain.Const;
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

namespace ApplicationService.Services.OrderProductServices
{
    public class OrderProductServices : IOrderProductServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOrderServices orderServices;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly IUserServices userServices;
        private readonly IRepository<OrderProducts> repository;
        public OrderProductServices(IUnitOfWork unitOfWork, IOrderServices orderServices, IHttpContextAccessor httpContextAccessor, IMapper mapper, IUserServices userServices)
        {
            this.unitOfWork = unitOfWork;
            this.orderServices = orderServices;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.userServices = userServices;
            this.repository = unitOfWork.GetRepository<OrderProducts>();
        }
        public async Task<List<OrderProductsResponseOrderVM>> GetAllOrdersAsync()
        {
            var orderProudcts = await repository.GetAllIncludingAsync(o => o.Order, o => o.Department, o => o.OfficeBoy);
            if (orderProudcts is not null)
            {
                return mapper.Map<List<OrderProductsResponseOrderVM>>(orderProudcts);
            }
            return null;
        }

        public async Task<List<OrdersDetailsResponeVM>> GetAllOrdersWithoutStatusAsync()
        {
            var orderProudcts = await repository.GetAllIncludingAsync(i => i.Status == (int)OrderStatus.None, o => o.Order, o => o.Product, o => o.Department, o => o.OfficeBoy);
            if (orderProudcts is not null)
            {
                return mapper.Map<List<OrdersDetailsResponeVM>>(orderProudcts);
            }
            return null;
        }
        public async Task<List<OrdersDetailsResponeVM>> GetAllOrdersForTheCurrentOfficeBoyThatWereNotDelivered()
        {
            var UserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var orderProducts = await repository.GetAllIncludingAsync(
                                        i => i.OfficeBoyId == UserId &&
                                        (i.Status != (int)OrderStatus.Delivered && i.Status != (int)OrderStatus.Denied),
                                        o => o.Order, o => o.Product, o => o.Department, o => o.OfficeBoy);

            if (orderProducts is not null)
            {
                return mapper.Map<List<OrdersDetailsResponeVM>>(orderProducts);
            }
            return null;
        }

        public async Task<List<OrdersDetailsResponeVM>> GetAllOrdersForTheCurrentOfficeBoyThatWereNotPaid()
        {
            var UserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var orderProducts = await repository.GetAllIncludingAsync(
                                      i => (i.OfficeBoyId == UserId &&
                                            !i.IsPaid &&
                                            i.Status == (int)OrderStatus.Delivered),
                                      o => o.Order, o => o.Product, o => o.Department, o => o.OfficeBoy);

            if (orderProducts != null)
            {
                return mapper.Map<List<OrdersDetailsResponeVM>>(orderProducts);
            }
            return null;
        }



        public async Task<List<OrderProductsResponseOrderVM>> GetAllUnpaidOrdersAsync()
        {
            var orderProudcts = await repository.GetAllIncludingAsync(i => i.Status == (int)OrderStatus.Delivered && !i.IsPaid, o => o.Order, o => o.Department, o => o.OfficeBoy);
            if (orderProudcts is not null)
            {
                return mapper.Map<List<OrderProductsResponseOrderVM>>(orderProudcts);
            }
            return null;
        }


        public async Task<List<OrderProductsResponseOrderVM>> GetAllOrdersForUserAsync(string userId)
        {
            var orderProudcts = await repository.GetAllIncludingAsync(o => o.Order.UserId == userId, o => o.Order, o => o.Department, o => o.OfficeBoy);
            if (orderProudcts is not null)
            {
                return mapper.Map<List<OrderProductsResponseOrderVM>>(orderProudcts);
            }
            return null;
        }

        public async Task<List<OrderProductsResponseOrderVM>> GetCurrentUserOrdersAsync()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var orderProudcts = await repository.GetAllIncludingAsync(o => o.Order.UserId == userId, o => o.Order, o => o.Department, o => o.OfficeBoy, o => o.Product);
            if (orderProudcts is not null)
            {
                return mapper.Map<List<OrderProductsResponseOrderVM>>(orderProudcts);
            }
            return null;
        }

        public async Task<int> EditStatus(OrderEditStatusRequestVM model)
        {
            var productOrderToUpdateStatus = await repository.GetIncludingAsync((i => i.ProductId == model.ProductId && i.OrderId == model.OrderId), i => i.Product);
            if (productOrderToUpdateStatus is not null)
            {
                productOrderToUpdateStatus.Status = model.Status;
                repository.Update(productOrderToUpdateStatus);
                if (model.Status == (int)(OrderStatus.Delivered))
                {
                    var order = await orderServices.GetOrderAsync(model.OrderId);
                    if (productOrderToUpdateStatus.IsGuest)
                    {
                        order.GuestTotalPrice += (productOrderToUpdateStatus.Quantity * productOrderToUpdateStatus.Product.Price);
                    }
                    else
                    {
                        order.EmployeeTotalPrice += (productOrderToUpdateStatus.Quantity * productOrderToUpdateStatus.Product.Price);

                    }

                    orderServices.Update(order);
                }
                return await unitOfWork.SaveChangesAsync();

            }

            return 0;
        }

        public async Task<int> EditIsPaid(OrderEditIsPaidRequestVM model)
        {
            var productOrderToUpdateIspaid = await repository.GetByAsync(i => i.ProductId == model.ProductId && i.OrderId == model.OrderId);
            if (productOrderToUpdateIspaid is not null)
            {
                productOrderToUpdateIspaid.IsPaid = model.IsPaid;
                repository.Update(productOrderToUpdateIspaid);
                return await unitOfWork.SaveChangesAsync();

            }
            return 0;
        }



        public async Task<int> CancelOrderProduct(CancelOrderProductRequestVM orderProductRequestVM)
        {

            var UserId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var productOrderToCancel = await repository.GetIncludingAsync((i => i.ProductId == orderProductRequestVM.ProductId && i.Order.UserId == UserId && i.OrderId == orderProductRequestVM.OrderId), i => i.Product);
            if (productOrderToCancel is not null)
            {

                if (productOrderToCancel.Status != 0)
                {
                    return -4;
                }
                repository.Delete(productOrderToCancel);

                await unitOfWork.SaveChangesWithoutDisposeAsync();
                var order = await orderServices.GetOrderAsync(orderProductRequestVM.OrderId);
                if (!await repository.AnyAsync(i => i.OrderId == order.Id))
                {
                    orderServices.Delete(order);
                    return await unitOfWork.SaveChangesAsync();
                }
                if (productOrderToCancel.IsGuest)
                {
                    order.GuestTotalPrice -= (productOrderToCancel.Product.Price * productOrderToCancel.Quantity);
                }
                else
                {
                    order.EmployeeTotalPrice -= (productOrderToCancel.Product.Price * productOrderToCancel.Quantity);

                }
                orderServices.Update(order);
                return await unitOfWork.SaveChangesAsync();

            }
            return -5;
        }

        public async Task<List<OrderReportHeaderVM>> GetReportOrdersHeaderSearch(ReportHeaderSearchRequestVM model)
        {
            if (!string.IsNullOrEmpty(model.UserId) && model.SearchOptions == SearchOptions.Employee)
            {
                var orderProudcts = await repository.GetAllIncludingAsync((o => o.Order.UserId == model.UserId &&
                                                                             !o.IsGuest &&
                                                                             o.IsPaid == model.IsPaid &&
                                                                             o.Status == (int)OrderStatus.Delivered &&
                                                                             (model.From == null || o.Order.CreatedDate >= model.From) &&
                                                                             (model.To == null || o.Order.CreatedDate <= model.To)),
                                                                         o => o.Order, o => o.Order.User, o => o.Department, o => o.OfficeBoy);
                if (orderProudcts is not null)
                {
                    return mapper.Map<List<OrderReportHeaderVM>>(orderProudcts);
                }
                return null;
            }
            else if (model.SearchOptions == SearchOptions.IsGuest)
            {
                var orderProudcts = await repository.GetAllIncludingAsync((o => o.IsGuest &&
                                                                             o.IsPaid == model.IsPaid &&
                                                                             o.Status == (int)OrderStatus.Delivered &&
                                                                             (model.From == null || o.Order.CreatedDate >= model.From) &&
                                                                             (model.To == null || o.Order.CreatedDate <= model.To)),
                                                                         o => o.Order, o => o.Order.User, o => o.Department, o => o.OfficeBoy);
                if (orderProudcts is not null)
                {
                    return mapper.Map<List<OrderReportHeaderVM>>(orderProudcts);
                }
                return null;
            }
            else
            {
                return await GetAllOrdersHeadersDeliveredAsync(model.IsPaid, model.From, model.To);
            }
        }

        public async Task<List<OrderReportHeaderVM>> GetAllOrdersHeadersDeliveredAsync(bool isPaid, DateTime? from, DateTime? to)
        {
            var orderProudcts = await repository.GetAllIncludingAsync((o => o.Status == (int)OrderStatus.Delivered &&
                                                                             o.IsPaid == isPaid &&
                                                                             (from == null || o.Order.CreatedDate >= from) &&
                                                                             (to == null || o.Order.CreatedDate <= to)),
                                                                     o => o.Order, o => o.Order.User, o => o.Department, o => o.OfficeBoy);
            if (orderProudcts is not null)
            {
                return mapper.Map<List<OrderReportHeaderVM>>(orderProudcts);
            }
            return null;
        }

        public async Task<List<ReportOrderDetailsVM>> GetReportOrdersDetailsSearch(ReportDetailsSearchRequestVM model)
        {
            if (!string.IsNullOrEmpty(model.UserId) && model.SearchOptions == SearchOptions.Employee)
            {
                var orderProudcts = await repository.GetAllIncludingAsync((o => o.Order.UserId == model.UserId &&
                                                                             o.OrderId == model.OrderId &&
                                                                             !o.IsGuest &&
                                                                             o.IsPaid == model.isPaid &&
                                                                             o.Status == (int)OrderStatus.Delivered &&
                                                                             (model.From == null || o.Order.CreatedDate >= model.From) &&
                                                                             (model.To == null || o.Order.CreatedDate <= model.To)),
                                                                         o => o.Order, o => o.Order.User, o => o.Department, o => o.Product, o => o.OfficeBoy);
                if (orderProudcts is not null)
                {
                    return mapper.Map<List<ReportOrderDetailsVM>>(orderProudcts);
                }
                return null;
            }
            else if (model.SearchOptions == SearchOptions.IsGuest)
            {
                var orderProudcts = await repository.GetAllIncludingAsync((o => o.IsGuest &&
                                                                             o.OrderId == model.OrderId &&
                                                                             o.IsPaid == model.isPaid &&
                                                                             o.Status == (int)OrderStatus.Delivered &&
                                                                             (model.From == null || o.Order.CreatedDate >= model.From) &&
                                                                             (model.To == null || o.Order.CreatedDate <= model.To)),
                                                                         o => o.Order, o => o.Order.User, o => o.Department, o => o.Product, o => o.OfficeBoy);
                if (orderProudcts is not null)
                {
                    return mapper.Map<List<ReportOrderDetailsVM>>(orderProudcts);
                }
                return null;
            }
            else
            {
                return await GetAllOrdersDeliveredDetailsAsync(model.OrderId, model.isPaid, model.From, model.To);
            }
        }

        public async Task<List<ReportOrderDetailsVM>> GetAllOrdersDeliveredDetailsAsync(int orderId, bool isPaid, DateTime? from, DateTime? to)
        {
            var orderProudcts = await repository.GetAllIncludingAsync((o => o.Status == (int)OrderStatus.Delivered &&
                                                                             o.OrderId == orderId &&
                                                                             o.IsPaid == isPaid &&
                                                                             (from == null || o.Order.CreatedDate >= from) &&
                                                                             (to == null || o.Order.CreatedDate <= to)),
                                                                     o => o.Order, o => o.Product, o => o.Order.User, o => o.Department, o => o.OfficeBoy);
            if (orderProudcts is not null)
            {
                return mapper.Map<List<ReportOrderDetailsVM>>(orderProudcts);
            }
            return null;
        }




        public async Task<List<ReportEmployeePrintVM>> PrintEmployeeReport(ReportDetailsSearchRequestVM model)
        {
            if (!string.IsNullOrEmpty(model.UserId) && model.SearchOptions == SearchOptions.Employee)
            {
                List<OrderProducts> orderProducts = await repository.GetAllIncludingAsync((o => o.Order.UserId == model.UserId &&
                                                                             o.OrderId == model.OrderId &&
                                                                             !o.IsGuest &&
                                                                             o.IsPaid == model.isPaid &&
                                                                             o.Status == (int)OrderStatus.Delivered &&
                                                                             (model.From == null || o.Order.CreatedDate >= model.From) &&
                                                                             (model.To == null || o.Order.CreatedDate <= model.To)),
                                                                         o => o.Order, o => o.Order.User, o => o.Department, o => o.Product, o => o.OfficeBoy, o => o.Product.Category);
                if (orderProducts != null && orderProducts.Any())
                {
                    var reportVMsList = new List<ReportEmployeePrintVM>();

                    foreach (var orderProduct in orderProducts)
                    {
                        var reportVMs = mapper.Map<ReportEmployeePrintVM>(orderProduct);

                        var productDetails = orderProducts
                            .Where(op => op.OrderId == orderProduct.OrderId)
                            .Select(op => mapper.Map<ReportProductDetailsVM>(op))
                            .ToList();
                        reportVMs.ProductDetails = productDetails;

                        reportVMsList.Add(reportVMs);
                    }

                    return reportVMsList;
                }
            }

            return new List<ReportEmployeePrintVM>();
        }



        public async Task<List<ReportGuestPrintVM>> PrintGuestReport(ReportDetailsSearchRequestVM model)
        {
            if (model.SearchOptions == SearchOptions.IsGuest)
            {
                var orderProducts = await repository.GetAllIncludingAsync((o => o.IsGuest &&
                                                                             o.OrderId == model.OrderId &&
                                                                             o.IsPaid == model.isPaid &&
                                                                             o.Status == (int)OrderStatus.Delivered &&
                                                                             (model.From == null || o.Order.CreatedDate >= model.From) &&
                                                                             (model.To == null || o.Order.CreatedDate <= model.To)),
                                                                          o => o.Order, o => o.Order.User, o => o.Department, o => o.Product, o => o.OfficeBoy, o => o.Product.Category);
                if (orderProducts != null && orderProducts.Any())
                {
                    var reportVMsList = new List<ReportGuestPrintVM>();

                    foreach (var orderProduct in orderProducts)
                    {
                        var reportVMs = mapper.Map<ReportGuestPrintVM>(orderProduct);

                        var productDetails = orderProducts
                            .Where(op => op.OrderId == orderProduct.OrderId)
                            .Select(op => mapper.Map<ReportProductDetailsVM>(op))
                            .ToList();
                        reportVMs.ProductDetails = productDetails;

                        reportVMsList.Add(reportVMs);
                    }

                    return reportVMsList;
                }
            }

            return null;
        }

        public async Task<List<ReportAllOrderVM>> PrintAllAsync(int orderId, bool isPaid, DateTime? from, DateTime? to)
        {
                var orderProducts = await repository.GetAllIncludingAsync((o => o.Status == (int)OrderStatus.Delivered &&
                                                                             o.OrderId == orderId &&
                                                                             o.IsPaid == isPaid &&
                                                                             (from == null || o.Order.CreatedDate >= from) &&
                                                                             (to == null || o.Order.CreatedDate <= to)),
                                                                     o => o.Order, o => o.Product, o => o.Order.User, o => o.Department, o => o.OfficeBoy, o => o.Product.Category);
                if (orderProducts != null && orderProducts.Any())
                {
                    var reportVMsList = new List<ReportAllOrderVM>();

                    foreach (var orderProduct in orderProducts)
                    {
                        var reportVMs = mapper.Map<ReportAllOrderVM>(orderProduct);

                        var productDetails = orderProducts
                            .Where(op => op.OrderId == orderProduct.OrderId)
                            .Select(op => mapper.Map<ReportProductDetailsVM>(op))
                            .ToList();
                        reportVMs.ProductDetails = productDetails;

                        reportVMsList.Add(reportVMs);
                    }

                    return reportVMsList;
                }

            return null;
        }


    }
}
