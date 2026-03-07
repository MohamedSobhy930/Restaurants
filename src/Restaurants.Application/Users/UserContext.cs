using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Restaurants.Application.Users
{
    public interface IUserContext
    {
        public CurrentUser? GetCurrentUser();
    }
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            var user = httpContextAccessor?.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("user context is not present");
            }
            if(user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var Email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var Roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);
            var Nationality = user.Claims.FirstOrDefault(c => c.Type == "Nationality")?.Value;
            var DateOfBirthstring = user.Claims.FirstOrDefault(c => c.Type == "DateOfBirth")?.Value;
            var DateOfBirth = DateOfBirthstring == null ? (DateOnly?) null: DateOnly.ParseExact(DateOfBirthstring , "yyyy-MM-dd") ;

            return new CurrentUser(userId, Email, Roles , Nationality , DateOfBirth); 
        }
    }
}
