using Castle.DynamicProxy;
using Goverment.AuthApi.Commans.AOP.Intercept;
using System.Transactions;

namespace Goverment.AuthApi.Commans.AOP.Transaction
{
    public class TransactionAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                invocation.Proceed();
                transactionScope.Complete();
            }
            catch (Exception e)
            {
                transactionScope.Dispose();
                throw;
            }
        }
    }
}
