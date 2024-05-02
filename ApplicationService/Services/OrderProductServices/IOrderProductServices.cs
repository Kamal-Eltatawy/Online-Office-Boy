using ApplicationService.Services.UserServices;
using ApplicationService.ViewModels;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Reposatory;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.OrderProductServices
{
    public interface IOrderProductServices
    {
        Task<int> CancelOrderProduct(CancelOrderProductRequestVM orderProductRequestVM);
        Task<int> EditStatus(OrderEditStatusRequestVM model);
        Task<List<OrderReportHeaderVM>> GetAllOrdersHeadersDeliveredAsync(bool isPaid, DateTime? from, DateTime? to);
        Task<List<OrderProductsResponseOrderVM>> GetAllOrdersForUserAsync(string userId);
        Task<List<OrdersDetailsResponeVM>> GetAllOrdersForTheCurrentOfficeBoyThatWereNotDelivered();
        Task<List<OrdersDetailsResponeVM>> GetAllOrdersWithoutStatusAsync();
        Task<List<OrderProductsResponseOrderVM>> GetAllUnpaidOrdersAsync();
        Task<List<OrderProductsResponseOrderVM>> GetCurrentUserOrdersAsync();
        Task<List<OrderReportHeaderVM>> GetReportOrdersHeaderSearch(ReportHeaderSearchRequestVM model);
        Task<List<ReportOrderDetailsVM>> GetAllOrdersDeliveredDetailsAsync(int orderID, bool isPaid, DateTime? from, DateTime? to);
        Task<List<ReportOrderDetailsVM>> GetReportOrdersDetailsSearch(ReportDetailsSearchRequestVM model);
        Task<List<OrderProductsResponseOrderVM>> GetAllOrdersAsync();
        Task<int> EditIsPaid(OrderEditIsPaidRequestVM model);
        Task<List<OrdersDetailsResponeVM>> GetAllOrdersForTheCurrentOfficeBoyThatWereNotPaid();
        Task<List<ReportEmployeePrintVM>> PrintEmployeeReport(ReportDetailsSearchRequestVM model);
        Task<List<ReportGuestPrintVM>> PrintGuestReport(ReportDetailsSearchRequestVM model);
        Task<List<ReportAllOrderVM>> PrintAllAsync(int orderId, bool isPaid, DateTime? from, DateTime? to);
    }
}
