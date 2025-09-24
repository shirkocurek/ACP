namespace c971_mobile_application_development_using_c_sharp.Helpers;

public static class ServiceHelper
{
    public static IServiceProvider Services { get; set; } = default!;

    public static T GetService<T>() where T : class =>
        Services.GetService(typeof(T)) as T
            ?? throw new InvalidOperationException($"Service of type {typeof(T)} not registered.");
}

