using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restraurants.Domain.Entities;
using Restraurants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Restaurants.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand , bool>
    {
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private IUserContext _userContext;
        private UserManager<User> _userStore;
        public UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger,
            IUserContext userContext,
        UserManager<User> userStore
            )
        {
            _logger = logger;
            _userContext = userContext;
            _userStore = userStore;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();
            _logger.LogInformation("Update User :{@userId} with : {Request} ", user!.Id, request);

            var userFromDb =await _userStore.FindByIdAsync(user.Id );
            if (userFromDb == null)
            {
                return false;
            }
            userFromDb.DateOfBirth = request.DateOfBirth;
            userFromDb.Nationality = request.Nationality;
            await _userStore.UpdateAsync(userFromDb);
            return true;
        }
    }
}
