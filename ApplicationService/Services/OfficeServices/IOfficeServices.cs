using ApplicationService.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.DepartmentServices
{
    public interface IOfficeServices
    {
        Task<int> CreateAsync(OfficeCreateRequestVM productRequestVM);
        Task<int> UpdateAsync(EditOfficeRequestVM model);
        public Task< List<Office>> GetAllAsync();
        Task<List<OfficeCartVM>> GetOfficesAsync();
        Task<EditOfficeRequestVM> GetAsync(int officeId);
    }
}
