using Microsoft.Extensions.DependencyInjection;
using System;

namespace c971_mobile_application_development_using_c_sharp.Helpers
{
    public static class ServiceHelper
    {
        public static IServiceProvider Services { get; private set; } = default!;

        public static void Initialize(IServiceProvider services)
        {
            Services = services;
        }

        public static T GetService<T>() where T : notnull =>
            Services.GetRequiredService<T>();
    }
}


