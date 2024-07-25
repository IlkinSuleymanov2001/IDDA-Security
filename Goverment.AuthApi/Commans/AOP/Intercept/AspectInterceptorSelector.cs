using Castle.DynamicProxy;
using System.Reflection;
namespace Goverment.AuthApi.Commans.AOP.Intercept
{
    
        public class AspectInterceptorSelector : IInterceptorSelector
        {
            public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
            {
            var classAttributes = type.
                GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();

            var methodAttributes = type.GetMethods()
                .FirstOrDefault(m => m.Name == method.Name && ParametersMatch(m.GetParameters(), method.GetParameters()))
                ?.GetCustomAttributes<MethodInterceptionBaseAttribute>(true);

            if (methodAttributes != null)
            {
                classAttributes.AddRange(methodAttributes);
            }

            // Filter out FluentValidation interceptors
            classAttributes = classAttributes.Where(attr => !IsFluentValidationInterceptor(attr)).ToList();

            return classAttributes.OrderBy(x => x.Priority).ToArray();

            /*            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                            (true).ToList();
                        var methodAttributes = type.GetMethod(method.Name)
                            .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
                        classAttributes.AddRange(methodAttributes);

                        return classAttributes.OrderBy(x => x.Priority).ToArray();*/
        }

        private bool ParametersMatch(ParameterInfo[] parameters1, ParameterInfo[] parameters2)
        {
            if (parameters1.Length != parameters2.Length)
                return false;

            for (int i = 0; i < parameters1.Length; i++)
            {
                if (parameters1[i].ParameterType != parameters2[i].ParameterType)
                    return false;
            }

            return true;
        }

        private bool IsFluentValidationInterceptor(MethodInterceptionBaseAttribute attribute)=>
             attribute.GetType().Namespace.Contains("FluentValidation");

    }
}
