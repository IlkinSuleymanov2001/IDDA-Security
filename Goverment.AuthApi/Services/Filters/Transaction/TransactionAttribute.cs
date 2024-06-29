namespace Goverment.AuthApi.Services.Filters.Transaction
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Transactions;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class TransactionAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                try
                {
                    var resultContext = await next();
                    scope.Complete();
                }
                catch(Exception)
                {
                    scope.Dispose();
                    throw;
                }
            }
        }
    }

}
