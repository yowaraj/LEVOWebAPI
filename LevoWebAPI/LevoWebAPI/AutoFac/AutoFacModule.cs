using Autofac;
using LevoWebAPI.IoC;
using System;

namespace LevoWebAPI.AutoFac
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder
                .RegisterAssemblyTypes(assemblies)
                .AssignableTo<IService>()
                .AsImplementedInterfaces();
        }
    }
}