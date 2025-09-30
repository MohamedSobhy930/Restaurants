using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users.Commands.UpdateUser;
using Restraurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.Commands.AssignUserRole
{
    public class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand , bool>
    {
        private readonly ILogger<AssignUserRoleCommandHandler> _logger;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;
        public AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> logger,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<bool> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("assigning user role {@Request}" , request);

            var user =await _userManager.FindByEmailAsync(request.UserEmail);
            if (user == null) 
                return false;
            var role = await _roleManager.FindByNameAsync(request.RoleName);
            if (role == null) 
                return false;
            await _userManager.AddToRoleAsync(user, role.Name!);
            return true;
        }
    }
}
