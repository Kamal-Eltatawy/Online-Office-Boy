using AutoMapper;
using System.Drawing.Drawing2D;
using System.Drawing;

using System.Security.Claims;
using ApplicationService.ViewModels;
using Domain.Entities;
using Domain.Const;

namespace ApplicationService.Mapper
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            #region UserMapping
            CreateMap<UserViewModelRequest, User>()
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
             .ForMember(dest => dest.PaidPeriod, opt => opt.MapFrom(src => src.PaidPeriod))
             .ForMember(dest => dest.OfficeId, opt => opt.MapFrom(src => src.OfficeID)).ReverseMap();


            CreateMap<User, OfficeBoyResponseVM>()
             .ForMember(dest => dest.OfficeBoyId, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
             .ForMember(dest => dest.PaidPeriod, opt => opt.MapFrom(src => (PaidPeriod)src.PaidPeriod))
             .ForMember(dest => dest.ShiftStartTime, opt => opt.MapFrom(src => src.Shifts.ShiftStartTime))
             .ForMember(dest => dest.ShiftEndTime, opt => opt.MapFrom(src => src.Shifts.ShiftEndTime))
             .ForMember(dest => dest.ShiftId, opt => opt.MapFrom(src => src.ShiftId))
             .ForMember(dest => dest.OfficeLocation, opt => opt.MapFrom(src => src.Office.Location)).ReverseMap();

            CreateMap<User, UserWithRolesResponseVM>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
             .ForMember(dest => dest.PaidPeriod, opt => opt.MapFrom(src => (PaidPeriod)src.PaidPeriod))
             .ForMember(dest => dest.OfficeLocation, opt => opt.MapFrom(src => src.Office.Location)).ReverseMap();

            CreateMap<User, UserWithRolesRequestVM>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();


            CreateMap<User, OfficeBoyCreateShiftVM>()
 .ForMember(dest => dest.OfficeBoyId, opt => opt.MapFrom(src => src.Id))
 .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
 .ForMember(dest => dest.ShiftStartTime, opt => opt.MapFrom(src => src.Shifts.ShiftStartTime))
 .ForMember(dest => dest.ShiftEndTime, opt => opt.MapFrom(src => src.Shifts.ShiftEndTime)).ReverseMap();

            CreateMap<User, OfficeBoyUpdateShiftVM>()
             .ForMember(dest => dest.OfficeBoyId, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.ShiftStartTime, opt => opt.MapFrom(src => src.Shifts.ShiftStartTime))
             .ForMember(dest => dest.ShiftEndTime, opt => opt.MapFrom(src => src.Shifts.ShiftEndTime)).ReverseMap();




            CreateMap<User, OfficeBoyCartVM>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();


            CreateMap<User, UserSelectVM>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();

            CreateMap<User, OfficeCartVM>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OfficeId))
              .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Office.Location)).ReverseMap();


            #endregion

            #region Office

            CreateMap<Office, OfficeCartVM>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ID))
             .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location)).ReverseMap();


            CreateMap<OfficeCreateRequestVM, Office>()
             .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.IsKitchen, opt => opt.MapFrom(src => src.IsKitchen))
             .ReverseMap();

            CreateMap<EditOfficeRequestVM, Office>()
             .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.OfficeId))
             .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.IsKitchen, opt => opt.MapFrom(src => src.IsKitchen))
             .ReverseMap();



            CreateMap<User, OfficeCartVM>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Office.ID))
             .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Office.Location)).ReverseMap();




            #endregion

            #region Product

            CreateMap<Product, ProductResponseVM>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
             .ForMember(dest => dest.ReadyTime, opt => opt.MapFrom(src => src.ReadyTime))
             .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
             .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.PictureUrl))
             .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
             .ForMember(dest => dest.CategoryType, opt => opt.MapFrom(src => src.Category.Type))

             .ReverseMap();

            CreateMap<ProductRequestVM, Product>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                    .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable))
                    .ForMember(dest => dest.ReadyTime, opt => opt.MapFrom(src => src.ReadyTime))
                    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId)).ReverseMap();

            CreateMap<ProductEditRequestVM, Product>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
        .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable))
        .ForMember(dest => dest.PictureUrl, opt => opt.Ignore())
        .ForMember(dest => dest.ReadyTime, opt => opt.MapFrom(src => src.ReadyTime))
        .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.PictureUrl))
        .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId)).ReverseMap();
            #endregion

            #region Order

            CreateMap<OrderProductViewModel, OrderProducts>()
          .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
          .ForMember(dest => dest.IsGuest, opt => opt.MapFrom(src => src.IsGuest))
          .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
          .ForMember(dest => dest.OfficeBoyId, opt => opt.MapFrom(src => src.OfficeBoyId))
          .ForMember(dest => dest.DestiniationDepartmentId, opt => opt.MapFrom(src => src.DepartmentId)).ReverseMap();



            CreateMap<OrderProducts, OrderProductsResponseOrderVM>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order.Id))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ProductImgUrl, opt => opt.MapFrom(src => src.Product.PictureUrl))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))

            .ForMember(dest => dest.ReadyTime, opt => opt.MapFrom(src => src.Product.ReadyTime))
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => src.IsPaid))
            .ForMember(dest => dest.IsGuest, opt => opt.MapFrom(src => src.IsGuest))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (OrderStatus)src.Status))
            .ForMember(dest => dest.OfficeBoyName, opt => opt.MapFrom(src => src.OfficeBoy.Name))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
             .ReverseMap();

            CreateMap<OrderProducts, OrdersDetailsResponeVM>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order.Id))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ProductImgUrl, opt => opt.MapFrom(src => src.Product.PictureUrl))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
            .ForMember(dest => dest.ReadyTime, opt => opt.MapFrom(src => src.Product.ReadyTime))
            .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => src.IsPaid))
            .ForMember(dest => dest.IsGuest, opt => opt.MapFrom(src => src.IsGuest))
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.OfficeBoyName, opt => opt.MapFrom(src => src.OfficeBoy.Name))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
             .ReverseMap();







            #endregion

            #region Report

            CreateMap<OrderProducts, ReportOrderDetailsVM>()
         .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order.Id))
         .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
         .ForMember(dest => dest.ProductImgUrl, opt => opt.MapFrom(src => src.Product.PictureUrl))
         .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
         .ForMember(dest => dest.ReadyTime, opt => opt.MapFrom(src => src.Product.ReadyTime))
         .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
         .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
         .ForMember(dest => dest.IsPaid, opt => opt.MapFrom(src => src.IsPaid))
         .ForMember(dest => dest.IsGuest, opt => opt.MapFrom(src => src.IsGuest))
          .ForMember(dest => dest.Status, opt => opt.MapFrom(src => (OrderStatus)src.Status))
         .ForMember(dest => dest.OfficeBoyName, opt => opt.MapFrom(src => src.OfficeBoy.Name))
         .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
          .ReverseMap();

            CreateMap<OrderProducts, ReportEmployeePrintVM>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order.Id))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Order.CreatedDate.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.BillTo, opt => opt.MapFrom(src => src.Order.User.Name))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Order.EmployeeTotalPrice)).ReverseMap();

            CreateMap<OrderProducts, ReportGuestPrintVM>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order.Id))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Order.CreatedDate.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.CreatedName, opt => opt.MapFrom(src => src.Order.User.Name))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Order.EmployeeTotalPrice)).ReverseMap();

            CreateMap<OrderProducts, ReportAllOrderVM>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order.Id))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Order.CreatedDate.ToString("yyyy-MM-dd")))
            .ForMember(dest => dest.CreatedName, opt => opt.MapFrom(src => src.Order.User.Name))
            .ForMember(dest => dest.EmployeeTotalPrice, opt => opt.MapFrom(src => src.Order.EmployeeTotalPrice))
            .ForMember(dest => dest.GuestTotalPrice, opt => opt.MapFrom(src => src.Order.GuestTotalPrice))
            .ForMember(dest => dest.ProductDetails, opt => opt.MapFrom(src => src.Order.Id))
            .ReverseMap();

            CreateMap<OrderProducts, ReportProductDetailsVM>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.TotalProductPrice, opt => opt.MapFrom(src => (src.Product.Price * src.Quantity)))
                .ForMember(dest => dest.OfficeBoyName, opt => opt.MapFrom(src => src.OfficeBoy.Name))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.Product.Category.Type))

                .ForMember(dest => dest.Distination, opt => opt.MapFrom(src => src.Department.Location))
                .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Product.Description))
                 .ReverseMap();

            CreateMap<OrderProducts, OrderReportHeaderVM>()
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order.Id))
            .ForMember(dest => dest.EmployeeTotalPrice, opt => opt.MapFrom(src => src.Order.EmployeeTotalPrice))
            .ForMember(dest => dest.GuestTotalPrice, opt => opt.MapFrom(src => src.Order.GuestTotalPrice))
            .ForMember(dest => dest.CreatedName, opt => opt.MapFrom(src => src.Order.User.Name))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Order.CreatedDate.ToString("yyyy-MM-dd"))).ReverseMap();


            #endregion
        }


    }
}
