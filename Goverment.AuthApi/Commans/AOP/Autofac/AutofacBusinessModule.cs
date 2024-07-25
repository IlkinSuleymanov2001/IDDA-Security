using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Goverment.AuthApi.Commans.AOP.Intercept;

namespace Goverment.AuthApi.Commans.AOP.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
