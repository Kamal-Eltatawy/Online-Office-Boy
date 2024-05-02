using ApplicationService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.SiftsServices
{
    public interface IShiftServices
    {
        Task<int> CreateOfficeBoyShiftAsync(OfficeBoyCreateShiftVM model);
        Task<int> UpdatefficeBoyShiftsAsync(OfficeBoyUpdateShiftVM model);
    }
}
