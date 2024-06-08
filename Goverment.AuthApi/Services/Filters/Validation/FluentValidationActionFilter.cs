using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Goverment.AuthApi.Business.Filters.Validation;

public class FluentValidationActionFilter : IAsyncActionFilter
{
	private readonly IValidatorFactory _validatorFactory;

	public FluentValidationActionFilter(IValidatorFactory validatorFactory)
	{
		_validatorFactory = validatorFactory;
	}

	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		var arguments = context.ActionArguments.Values.ToList();
		var validators = arguments
			.Select(arg => _validatorFactory.GetValidator(arg.GetType()))
			.Where(validator => validator != null)
			.ToList();

		foreach (var validator in validators)
		{
			foreach (var argument in arguments)
			{
				if (validator.CanValidateInstancesOfType(argument.GetType()))
				{
					var validationResult = await validator.ValidateAsync(new ValidationContext<object>(argument));

					if (!validationResult.IsValid)
					{
						var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
						context.Result = new BadRequestObjectResult(new { message = "Validation failed.", errors });
						return;
					}
				}
			}
		}

		await next();
	}
}

