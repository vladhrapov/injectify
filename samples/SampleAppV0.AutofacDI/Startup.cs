using Autofac;
using Injectify.Abstractions;
using SampleAppV0.AutofacDI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAppV0.AutofacDI
{
    public sealed class Startup : IStartup<ContainerBuilder>
    {
        public void ConfigureServices(ContainerBuilder services)
        {
            services.RegisterType<SampleService>().As<ISampleService>().SingleInstance();
            services.RegisterType<StorageProvider>().As<IProvider>().InstancePerLifetimeScope();
            services.RegisterType<XmlProvider>().As<IProvider>().InstancePerLifetimeScope();
        }
    }
}
