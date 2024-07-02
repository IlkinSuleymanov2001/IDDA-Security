namespace Goverment.AuthApi.Commans.Filters.Transaction
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Transactions;

    //  [AttributeUsage(AttributeTargets.Method , Inherited = false, AllowMultiple = false)]
    public class TransactionAttribute : Attribute, IAsyncActionFilter
    {
        public TransactionAttribute()
        {
            Console.WriteLine("instance ");
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            using (var transactionScope = new TransactionScope())
            {
                ActionExecutedContext actionExecutedContext = await next();
                //if no exception were thrown
                if (actionExecutedContext.Exception == null)
                    transactionScope.Complete();
            }
        }
    }

}
