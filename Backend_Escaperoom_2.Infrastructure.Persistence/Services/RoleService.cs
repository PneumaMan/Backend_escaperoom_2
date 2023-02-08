using AutoMapper;
using Backend_Escaperoom_2.Application.Helpers;
using Backend_Escaperoom_2.Application.Interfaces;
using Backend_Escaperoom_2.Application.Interfaces.Services;
using Backend_Escaperoom_2.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend_Escaperoom_2.Infrastructure.Persistence.Services
{
    public class RoleService : IRoleService
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager, IDateTimeService dateTimeService,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _dateTimeService = dateTimeService;
            _mapper = mapper;
        }

        public async Task<Role> GetRoleById(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

    }
}
