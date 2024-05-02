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

namespace ApplicationService.Services.DepartmentServices
{
    public class OfficeServices : IOfficeServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly IRepository<Office>  repository;
        public OfficeServices(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.repository = unitOfWork.GetRepository<Office>();
        }
        public async Task<List<Office>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }
        public async Task<List<OfficeCartVM>> GetOfficesAsync()
        {
            return mapper.Map<List<OfficeCartVM>>(await repository.GetAllByAsync(d => d.IsKitchen));
        }
        public async Task<EditOfficeRequestVM> GetAsync(int officeId)
        {
            return mapper.Map<EditOfficeRequestVM>(await repository.GetByIdAsync(officeId));
        }

        public async Task<int> UpdateAsync(EditOfficeRequestVM model)
        {
            var office = await repository.GetByIdAsync(model.OfficeId);
            if (office is not null)
            {
                office.Name= model.Name;
                office.IsKitchen=model.IsKitchen;
                office.Location = model.Location;
                repository.Update(office);
                return await unitOfWork.SaveChangesAsync();
            }
            return -5;
        }

        public async Task<int> CreateAsync(OfficeCreateRequestVM productRequestVM)
        {

            await repository.AddAsync(mapper.Map<Office>(productRequestVM));

            return await unitOfWork.SaveChangesAsync();
        }




    }
}
