using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restraurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.Commands.DeleteUserRole
{
    public class DeleteUserRoleCommandHandler : IRequestHandler<DeleteUserRoleCommand, bool>
    {
        private readonly ILogger<DeleteUserRoleCommandHandler> _logger;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;
        public DeleteUserRoleCommandHandler(ILogger<DeleteUserRoleCommandHandler> logger,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<bool> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("delete user role {@Request}", request);

            var user =await _userManager.FindByEmailAsync(request.UserEmail);
            if (user == null) return false;

            var role = await _roleManager.FindByNameAsync(request.UserRole);
            if (role == null) return false;

            await _userManager.RemoveFromRoleAsync(user, role.Name!);
            return true;

        }
    }
}
