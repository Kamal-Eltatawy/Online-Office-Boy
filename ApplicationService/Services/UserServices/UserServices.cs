using ApplicationService.ViewModels;
using AutoMapper;
using Domain.Const;
using Domain.Entities;
using Infrastructure.Reposatory;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Services.UserServices
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly IRepository<User> repository;

        public UserServices(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,RoleManager<IdentityRole> roleManager , IMapper mapper, UserManager<User> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.userManager = userManager;
            this.repository = unitOfWork.GetRepository<User>();
        }

        public async Task<List<UserSelectVM>> GetAllUserInSelectVMAsync()
        {
            return mapper.Map<List<UserSelectVM>>(await repository.GetAllAsync());
        }
        public async Task<User> GetUserIncludingShiftAsync(string userId)
        {
            return await repository.GetIncludingAsync(i => i.Id == userId, i => i.Shifts);
        }
        public async Task<User> GetCurrentUserIncludingOfficeAsync()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await repository.GetIncludingAsync(i => i.Id == userId, i => i.Office);
        }


        public void UpdateUser(User user)
        {
            repository.Update(user);
        }

        public async Task<List<OfficeBoyCartVM>?> GetOfficeBoyWithCurrentShiftAsync(int officeId)
        {
            var usersWithShiftsAndDepartment = await repository.GetAllIncludingAsync(s => s.Shifts.ShiftStartTime <= DateTime.Now.TimeOfDay
                                                                                        && s.Shifts.ShiftEndTime >= DateTime.Now.TimeOfDay
                                                                                        && s.OfficeId == officeId
                                                                                            , u => u.Shifts);
            if (usersWithShiftsAndDepartment is not null)
            {
                return mapper.Map<List<OfficeBoyCartVM>>(usersWithShiftsAndDepartment);
            }
            return null;
        }

        public async Task<List<OfficeBoyCartVM>?> GetOfficeBoyWithCurrentShiftAsync()
        {
            var usersWithShiftsAndDepartment = await repository.GetAllIncludingAsync(s => s.Shifts.ShiftStartTime <= DateTime.Now.TimeOfDay
                                                                                        && s.Shifts.ShiftEndTime >= DateTime.Now.TimeOfDay
                                                                                            , u => u.Shifts);
            if (usersWithShiftsAndDepartment is not null)
            {
                return mapper.Map<List<OfficeBoyCartVM>>(usersWithShiftsAndDepartment);
            }
            return null;
        }

        public async Task<List<OfficeCartVM>?> GetOfficeBoysCurrentShiftOfficesAsync()
        {
            var usersWithShiftsAndDepartment = await repository.GetAllIncludingAsync(s => s.Shifts.ShiftStartTime <= DateTime.Now.TimeOfDay
                                                                                        && s.Shifts.ShiftEndTime >= DateTime.Now.TimeOfDay
                                                                                            , u => u.Shifts, u => u.Office);
            if (usersWithShiftsAndDepartment is not null)
            {
                return mapper.Map<List<OfficeCartVM>>(usersWithShiftsAndDepartment);
            }
            return null;
        }


        public async Task<List<OfficeBoyResponseVM>> GetAllOfficeBoysIncludingShiftsAndOfficeAsync()
        {
            var usersWithRoles = userManager.GetUsersInRoleAsync(Roles.OfficeBoy.ToString()).Result.ToList();

            var officeBoysWithDetails = await repository.GetAllIncludingAsync(
                u => usersWithRoles.Contains(u),
                u => u.Shifts,
                u => u.Office
            );

            return mapper.Map<List<OfficeBoyResponseVM>>(officeBoysWithDetails);
        }

        public async Task<List<UserWithRolesResponseVM>> GetAllUserIncludingOfficeAndRolesAsync()
        {
            var usersWithOffice = await repository.GetAllIncludingAsync(i => i.Office);

            var usersWithRoles = new List<UserWithRolesResponseVM>();

            foreach (var user in usersWithOffice)
            {
                var userWithRoles = new UserWithRolesResponseVM
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Name = user.Name,
                    PaidPeriod = Enum.GetName(typeof(PaidPeriod), user.PaidPeriod),
                    OfficeLocation = user.Office?.Location,
                    Roles = userManager.GetRolesAsync(user).Result.ToList()
                };

                usersWithRoles.Add(userWithRoles);
            }

            return usersWithRoles;
        }

        public async Task<UserWithRolesRequestVM> GetUserIncludingAsync(string userId)
        {
            var userWithOffice = await repository.GetIncludingAsync(i => i.Id == userId);
            if (userWithOffice != null)
            {
                return mapper.Map<UserWithRolesRequestVM>(userWithOffice);
            }
            return null;
        }

        #region AddRoles
        //public async Task<StatusResponseVM> AddUserToRoles(UserWithRolesRequestVM model)
        //{
        //    StatusResponseVM statusResponseVM = new StatusResponseVM();

        //    var user = await userManager.FindByIdAsync(model.Id);
        //    if (user == null)
        //    {
        //        statusResponseVM.Code = 0;
        //        statusResponseVM.Message = "User Not Found";
        //        return statusResponseVM; 
        //    }

        //    var existingRoles = new HashSet<string>(await userManager.GetRolesAsync(user));
        //    var rolesToAdd = new HashSet<string>(model.Roles);
        //    var invalidRoles = rolesToAdd.Except( roleManager.Roles.Select(i=>i.Name)).ToList();
        //    if (invalidRoles.Any())
        //    {
        //        statusResponseVM.Code = -1;
        //        statusResponseVM.Message = $"Invalid roles: {string.Join(", ", invalidRoles)}";
        //        return statusResponseVM; 
        //    }
        //    var rolesToAddList = rolesToAdd.Except(existingRoles).ToList(); // Efficiently determine roles to add
        //    if (rolesToAddList.Any())
        //    {
        //        var result = await userManager.AddToRolesAsync(user, rolesToAddList);
        //        if (!result.Succeeded)
        //        {
        //            statusResponseVM.Code = -2;
        //            statusResponseVM.Message = "Error adding roles to user";
        //            return statusResponseVM; 
        //        }
        //    }
        //    statusResponseVM.Code = 1;
        //    statusResponseVM.Message = "Roles added successfully";
        //    return statusResponseVM;
        //}

        #endregion

        public async Task<StatusResponseVM> AddUserToRoles(UserWithRolesRequestVM model)
        {
            StatusResponseVM statusResponseVM = new StatusResponseVM();
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                statusResponseVM.Code = 0;
                statusResponseVM.Message = "User Not Found";
                return statusResponseVM;
            }

            var existingRoles = await userManager.GetRolesAsync(user);

            var rolesToAdd = model.Roles.Except(existingRoles);

            // Add the roles that the user does not currently have
            foreach (var roleName in rolesToAdd)
            {
                // Check if the role exists
                if (await roleManager.RoleExistsAsync(roleName))
                {
                    // Add the role to the user
                    var result = await userManager.AddToRoleAsync(user, roleName);
                    if (!result.Succeeded)
                    {
                        statusResponseVM.Code = -1;
                        statusResponseVM.Message = $"Failed to add role: {roleName}";
                        return statusResponseVM;
                    }
                }
                else
                {
                    statusResponseVM.Code = -1;
                    statusResponseVM.Message = $"Invalid role: {roleName}";
                    return statusResponseVM;
                }
            }

            var rolesToRemove = existingRoles.Except(model.Roles);

            foreach (var roleName in rolesToRemove)
            {
                var result = await userManager.RemoveFromRoleAsync(user, roleName);
                if (!result.Succeeded)
                {
                    statusResponseVM.Code = -1;
                    statusResponseVM.Message = $"Failed to remove role: {roleName}";
                    return statusResponseVM;
                }
            }

            statusResponseVM.Code = 1;
            statusResponseVM.Message = "Saved successfully";
            return statusResponseVM;
        }


        public async Task<OfficeBoyUpdateShiftVM?> GetOfficeBoyByIdIncludingShiftAsync(string officeBoyId)
        {
            var officeBoy = await repository.GetIncludingAsync(u => u.Id == officeBoyId, u => u.Shifts);
            if (officeBoy == null || !await userManager.IsInRoleAsync(officeBoy, Roles.OfficeBoy.ToString()))
            {
                return null;
            }
            return mapper.Map<OfficeBoyUpdateShiftVM>(officeBoy);
        }

        public async Task<OfficeBoyCreateShiftVM?> GetOfficeBoyByIdAsync(string officeBoyId)
        {
            var officeBoy = await repository.GetByAsync(o=>o.Id== officeBoyId);
            if (officeBoy == null || !await userManager.IsInRoleAsync(officeBoy, Roles.OfficeBoy.ToString()))
            {
                return null;
            }
            return mapper.Map<OfficeBoyCreateShiftVM>(officeBoy);
        }


        

 
    }


}

