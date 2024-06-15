using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Goverment.Core.CrossCuttingConcers.Results;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Goverment.AuthApi.Services.Filters.Validation
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
            var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors.Select(error => new {
                PropertyName = x.Key,
                error.ErrorMessage
            }))
            .GroupBy(x => x.PropertyName)  
            .Select(g => new {
                PropertyName = g.Key,
                ErrorMessages = g.Select(e => e.ErrorMessage).ToList(),
            })
            .ToList();


             /*   var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();
*/

                context.Result = new BadRequestObjectResult(new ErrorResult(errors, context.HttpContext.Request.Path, "Validation Exception"));
            }
        }
    }
}
