namespace Goverment.AuthApi.Commans.AOP.Transaction
{
    using System.Transactions;
    using AspectCore.DynamicProxy;

    public class TransactionAttribute : AbstractInterceptorAttribute
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await next(context);
            scope.Complete();
        }
    }
}
