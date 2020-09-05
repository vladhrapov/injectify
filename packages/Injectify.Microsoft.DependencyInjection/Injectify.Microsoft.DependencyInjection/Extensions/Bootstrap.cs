using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Injectify.Microsoft.DependencyInjection.Extensions
{
    public static class Bootstraper
    {
        public static void Bootstrap<TPage>(TPage page)
        {
            //BootstrapApp();
            var classInjectable = page.GetType().GetCustomAttribute<InjectableAttribute>();

            classInjectable.Bootstrap(page);
        }
    }
}
