using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Backend.Filters
{
    public class UserFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var service = context.HttpContext.RequestServices.GetService(typeof(IUserService)) as IUserService;
            var user = service.GetUserFromToken(context.HttpContext.Request).Result;
            if (user == null)
            {
                context.Result = new BadRequestObjectResult("Non Authorized");
            }
        }
    }
}
