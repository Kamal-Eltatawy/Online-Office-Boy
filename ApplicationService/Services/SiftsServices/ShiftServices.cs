using ApplicationService.Services.UserServices;
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

namespace ApplicationService.Services.SiftsServices
{
    public class ShiftServices : IShiftServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly IUserServices userServices;
        private readonly IRepository<Shifts> repository;

        public ShiftServices(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper, IUserServices userServices)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.userServices = userServices;
            this.repository = unitOfWork.GetRepository<Shifts>();
        }
        public async Task<int> CreateOfficeBoyShiftAsync(OfficeBoyCreateShiftVM model)
        {
            var officeBoy = await userServices.GetUserIncludingShiftAsync(model.OfficeBoyId);

            if (officeBoy is not null && officeBoy.ShiftId is null)
            {
                using (var transaction = await unitOfWork.BeginTransactionAsync())
                {
                    try
                    {
                        Shifts shifts = new Shifts()
                        {
                            ShiftStartTime = model.ShiftStartTime,
                            ShiftEndTime = model.ShiftEndTime,
                        };

                        await repository.AddAsync(shifts);
                        await unitOfWork.SaveChangesWithoutDisposeAsync();

                        officeBoy.ShiftId = shifts.ID;
                        userServices.UpdateUser(officeBoy);
                        await unitOfWork.SaveChangesWithoutDisposeAsync();

                        await transaction.CommitAsync();
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return 0;
                    }
                }
            }
            return -1;
        }



        public async Task<int> UpdatefficeBoyShiftsAsync(OfficeBoyUpdateShiftVM model)
        {
            var officeBoy = await userServices.GetUserIncludingShiftAsync(model.OfficeBoyId);
            if (officeBoy is not null && officeBoy.ShiftId is not null)
            {
                var officeBoyShift = await repository.GetByAsync(i => i.ID == officeBoy.ShiftId);
                officeBoyShift.ShiftStartTime = model.ShiftStartTime;
                officeBoyShift.ShiftEndTime = model.ShiftEndTime;
                repository.Update(officeBoyShift);
                return await unitOfWork.SaveChangesAsync();
            }
            return -1;
        }

    }
}
