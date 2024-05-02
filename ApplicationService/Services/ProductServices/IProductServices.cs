using ApplicationService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.ProductServices
{
    public interface IProductServices
    {
        Task<int> CreateAsync(ProductRequestVM productRequestVM, string rootPath);
        Task<List<ProductResponseVM>> GetAllAsync();
        Task<List<ProductResponseVM>> GetAllIncludingCategoryAsync();
        Task<ProductResponseVM> GetAsync(int id);
        Task<ProductEditRequestVM> GetIncludinyCategoyAsync(int ProductId);
        Task<int> UpdateAsync(ProductEditRequestVM ProductEditRequestVM, string rootPath);
    }
}
