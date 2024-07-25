using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Goverment.Core.CrossCuttingConcers.Resposne.Error;

namespace Goverment.AuthApi.Commans.Filters.Validation
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;

            var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                .SelectMany(v => v.Errors)
                .Select(v => v.ErrorMessage)
                .ToList().FirstOrDefault();

            context.Result = new BadRequestObjectResult(new ErrorResponse { Message = errors }.ToString());
        }
    }
}
