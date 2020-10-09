# Migration guide

## Migration guide for 0.x.x versions

Here you can find detailed information regarding version changes of 0.x.x versions, any new features, updates and improvements for the development usage.

## Version 0.3.0

Simplified DI bootstrap for `App` component, add `Frame` root provider for injecting dependencies smoothly and silently using `GetRootFrame` extension during page navigation.

**Before:**

```
// Mark application with bootstrap attribute
[UwpApplicationBootstrap]
sealed partial class App : Application, IUwpApplication<ServiceProvider> // Implement IUwpApplication 
{
    public App()
    {
        this.InitializeComponent();
        this.Suspending += OnSuspending;
        this.BootstrapStartup(); // Run Startup Bootstrap
    }

    public ServiceProvider Services { get; set; }

    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
        Frame rootFrame = Window.Current.Content as Frame;

        if (rootFrame == null)
        {
            rootFrame = new Frame();

            rootFrame.NavigationFailed += OnNavigationFailed;

            Window.Current.Content = rootFrame;
        }

        // Other code...
    }

    // Other code...

}
```

**After:**

```
using Injectify.Abstractions;
using Injectify.Microsoft.DependencyInjection;
using Injectify.Microsoft.DependencyInjection.Extensions;

// Mark application with bootstrap attribute
[UwpApplicationBootstrap]
sealed partial class App : Application, IUwpApplication<ServiceProvider> // Implement IUwpApplication 
{
    public App()
    {
        this.InitializeComponent();
        this.Suspending += OnSuspending;
        // [1] Removed BootstrapStartup
    }

    public ServiceProvider Services { get; set; }

    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
        Frame rootFrame = Window.Current.Content as Frame;

        if (rootFrame == null)
        {
            rootFrame = this.GetRootFrame(); // [2] Get frame with providers

            rootFrame.NavigationFailed += OnNavigationFailed;

            Window.Current.Content = rootFrame;
        }

        // Other code...
    }

    // Other code...

}
```

## Version 0.2.0

Simplified `IStartup` API, removed redundant generic types and properties.

**Before:**

```
using Injectify.Abstractions;
using Microsoft.Extensions.DependencyInjection;

// Implement IStartup interface as a Startup for the project
public sealed class Startup : IStartup<ServiceCollection, ServiceProvider>
{
    public Startup()
    {
        ConfigureServices(new ServiceCollection());
    }

    public ServiceProvider Services { get; set; }

    public ServiceProvider ConfigureServices(ServiceCollection services)
    {
        // Register dependencies
        services.AddScoped<ISampleService, SampleService>();
        services.AddSingleton<IProvider, StorageProvider>();
        services.AddSingleton<IProvider, XmlProvider>();

        // Build service provider
        Services = services.BuildServiceProvider();

        return Services;
    }
}
```

**After:**

```
using Injectify.Abstractions;
using Microsoft.Extensions.DependencyInjection;

// Implement IStartup interface as a Startup for the project
public sealed class Startup : IStartup<ServiceCollection> // [1] Removed ServiceProvider type
{
    // [2] Removed Services property

    // [3] Returns void, similar to Startup in ASP.NET Core
    public void ConfigureServices(ServiceCollection services)
    {
        // Register dependencies
        services.AddScoped<ISampleService, SampleService>();
        services.AddSingleton<IProvider, StorageProvider>();
        services.AddSingleton<IProvider, XmlProvider>();

        // [4] Removed service provider building logic
    }
}
```

## Version 0.1.0

Implement `IUwpApplication` contract and mark `App` using `UwpApplicationBootstrap` annotation. Use `BootstrapStartup` extension to bootstrap DI in the app.

```
using Injectify.Abstractions;
using Injectify.Microsoft.DependencyInjection;
using Injectify.Microsoft.DependencyInjection.Extensions;

// Mark application with bootstrap attribute
[UwpApplicationBootstrap]
sealed partial class App : Application, IUwpApplication<ServiceProvider> // Implement IUwpApplication 
{
    public App()
    {
        this.InitializeComponent();
        this.Suspending += OnSuspending;
        this.BootstrapStartup(); // Run Startup Bootstrap
    }

    // IUwpApplication implementation
    // Later on ServiceProvider will be injected here.
    public ServiceProvider Services { get; set; }

    // Other code...

}
```

Implement `IStartup<ServiceCollection, ServiceProvider>`, where all dependencies are registered in a single place. API is pretty similar to `Startup` from [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/startup?view=aspnetcore-3.1).

```
using Injectify.Abstractions;
using Microsoft.Extensions.DependencyInjection;

// Implement IStartup interface as a Startup for the project
public sealed class Startup : IStartup<ServiceCollection, ServiceProvider>
{
    public Startup()
    {
        ConfigureServices(new ServiceCollection());
    }

    public ServiceProvider Services { get; set; }

    public ServiceProvider ConfigureServices(ServiceCollection services)
    {
        // Register dependencies
        services.AddScoped<ISampleService, SampleService>();
        services.AddSingleton<IProvider, StorageProvider>();
        services.AddSingleton<IProvider, XmlProvider>();

        // Build service provider
        Services = services.BuildServiceProvider();

        return Services;
    }
}
```

Use `Injectable` and `Inject` annotations to inject registered dependencies in a page component within a `Bootstrap` execution in the constructor. `Inject` allows to pass all registered dependencies as `IEnumerable` of abstraction type.

```
using Injectify.Microsoft.DependencyInjection;
using Injectify.Microsoft.DependencyInjection.Extensions;

// Mark class as Injectable
[Injectable]
public sealed partial class MainPage : Page
{
    public MainPage()
    {
        Bootstraper.Bootstrap(this); // Run bootstrap for current page
        this.InitializeComponent();
    }

    // Mark property with Inject
    [Inject]
    public ISampleService SampleService { get; set; }

    // Mark property with Inject, multiple registered dependencies
    [Inject]
    public IEnumerable<IProvider> Providers { get; set; }
}
```

[Back](../README.md)