using ApplicationService.Services.ImageServices;
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

namespace ApplicationService.Services.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly IImageServices imageServices;
        private readonly IRepository<Product> repository;
        public ProductServices(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper, IImageServices imageServices)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.imageServices = imageServices;
            this.repository = unitOfWork.GetRepository<Product>();
        }

        public async Task<ProductResponseVM> GetAsync(int id)
        {
            return mapper.Map<ProductResponseVM>(await repository.GetByIdAsync(id));
        }

        public async Task<List<ProductResponseVM>> GetAllAsync()
        {
            return mapper.Map<List<ProductResponseVM>>(await repository.GetAllAsync());
        }
        public async Task<List<ProductResponseVM>> GetAllIncludingCategoryAsync()
        {
            return mapper.Map<List<ProductResponseVM>>(await repository.GetAllIncludingAsync(i=>i.Category));
        }
        public async Task<ProductEditRequestVM> GetIncludinyCategoyAsync(int ProductId)
        {
            return mapper.Map<ProductEditRequestVM>(await repository.GetIncludingAsync(i=>i.Id==ProductId,i=>i.Category));
        }
        public async Task<int> CreateAsync(ProductRequestVM productRequestVM, string rootPath)
        {
            var pictureUrl = await imageServices.UploadAsync(productRequestVM.File, rootPath);
            if (string.IsNullOrEmpty(pictureUrl))
            {
                return -1;
            }
            var product = mapper.Map<Product>(productRequestVM);

            product.PictureUrl = pictureUrl;

            await repository.AddAsync(product);

            return await unitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(ProductEditRequestVM ProductEditRequestVM, string rootPath)
        {
            var productToEdit = await repository.GetByIdAsync(ProductEditRequestVM.ProductId);

            if (productToEdit is null)
            {
                return -2;
            }
            if (ProductEditRequestVM.File is not null)
            {
               var pictureUrl = await imageServices.UploadAsync(ProductEditRequestVM.File, rootPath);
                if (string.IsNullOrEmpty(pictureUrl))
                {
                    return -1;
                }
                productToEdit.PictureUrl = pictureUrl;
            }
            productToEdit.Name = ProductEditRequestVM.Name;
            productToEdit.Description = ProductEditRequestVM.Description;
            productToEdit.Price = ProductEditRequestVM.Price;
            productToEdit.IsAvailable = ProductEditRequestVM.IsAvailable;
            productToEdit.ReadyTime = ProductEditRequestVM.ReadyTime;
            productToEdit.CategoryId = ProductEditRequestVM.CategoryId;

             repository.Update(productToEdit);

            return await unitOfWork.SaveChangesAsync();
        }




    }
}
