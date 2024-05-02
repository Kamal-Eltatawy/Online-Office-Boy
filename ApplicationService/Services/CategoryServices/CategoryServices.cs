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

namespace ApplicationService.Services.CategoryServices
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly IRepository<Category> repository;
        public CategoryServices(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.repository = unitOfWork.GetRepository<Category>();
        }
        public async Task<List<Category>> GetAllAsync()
        {
            return await repository.GetAllAsync();
        }
   

 



    }
}

