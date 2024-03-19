using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdministrativoImperial.Portal.Utils.Attribute
{
	public class AutenticadoAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.HttpContext.User.Identity.IsAuthenticated)
			{
				context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
				return; 
			}

			base.OnActionExecuting(context);
		}
	}
}
