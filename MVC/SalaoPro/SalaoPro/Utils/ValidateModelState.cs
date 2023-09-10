using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalaoPro.Portal.Utils
{
    public class ValidateModelState : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
