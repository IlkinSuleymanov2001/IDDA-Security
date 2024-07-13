using Castle.DynamicProxy;
using FluentValidation;
using Goverment.AuthApi.Commans.AOP.Intercept;

namespace Goverment.AuthApi.Commans.AOP.Validaion
{
    public class ValidationAspect : MethodInterception
    {
        private readonly Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            // defending way 
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
                throw new Exception(" incoming validation object referance  is not IValidator referance");
            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType)!;
            var entityType = _validatorType.BaseType?.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
