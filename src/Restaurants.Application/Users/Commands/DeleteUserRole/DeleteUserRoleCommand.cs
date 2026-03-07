using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.Commands.DeleteUserRole
{
    public class DeleteUserRoleCommand : IRequest<bool>
    {
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
    }
}
