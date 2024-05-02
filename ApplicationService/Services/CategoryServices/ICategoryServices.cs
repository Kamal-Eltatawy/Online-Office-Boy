using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.CategoryServices
{
    public interface ICategoryServices
    {
        Task<List<Category>> GetAllAsync();
    }
}
