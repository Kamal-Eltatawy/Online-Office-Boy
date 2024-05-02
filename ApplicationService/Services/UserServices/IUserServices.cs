using ApplicationService.ViewModels;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.UserServices
{
    public interface IUserServices
    {
        Task<StatusResponseVM> AddUserToRoles(UserWithRolesRequestVM model);
        Task<List<OfficeBoyResponseVM>> GetAllOfficeBoysIncludingShiftsAndOfficeAsync();
        Task<List<UserWithRolesResponseVM>> GetAllUserIncludingOfficeAndRolesAsync();
        Task<List<UserSelectVM>> GetAllUserInSelectVMAsync();
        Task<User> GetCurrentUserIncludingOfficeAsync();
        Task<OfficeBoyCreateShiftVM?> GetOfficeBoyByIdAsync(string officeBoyId);
        Task<OfficeBoyUpdateShiftVM?> GetOfficeBoyByIdIncludingShiftAsync(string officeBoyId);
        Task<List<OfficeCartVM>?> GetOfficeBoysCurrentShiftOfficesAsync();
        Task<List<OfficeBoyCartVM>?> GetOfficeBoyWithCurrentShiftAsync(int officeId);
        Task<List<OfficeBoyCartVM>?> GetOfficeBoyWithCurrentShiftAsync();
        Task<UserWithRolesRequestVM> GetUserIncludingAsync(string userId);
        Task<User> GetUserIncludingShiftAsync(string userId);
        void UpdateUser(User user);

    }
}
