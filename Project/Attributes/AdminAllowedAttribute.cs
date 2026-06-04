using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Project.Attributes
{
    public class AdminAllowedAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userRepo = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
            var vkIdClaim = context.HttpContext.User.FindFirst("vkId")?.Value;

            if (string.IsNullOrEmpty(vkIdClaim))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var user = await userRepo.GetByVkIdAsync(vkIdClaim);
            if (user == null || !user.IsAdmin)
            {
                throw new Exception("Access denied: admin required");
            }

            await next();
        }
    }
}