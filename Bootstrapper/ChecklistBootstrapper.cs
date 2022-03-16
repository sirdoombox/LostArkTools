using System.Windows;
using System.Windows.Threading;
using LostArkTools.Features.Root;
using LostArkTools.Features.Shared;
using LostArkTools.Services;
using LostArkTools.Services.Base;
using StyletIoC;

namespace LostArkTools.Bootstrapper;

public class ChecklistBootstrapper : Bootstrapper<ApplicationRootViewModel>
{
    protected override void ConfigureIoC(IStyletIoCBuilder builder)
    {
        builder.Bind<ILocalStorageService>().ToAllImplementations().InSingletonScope();
        builder.Bind<FeatureScreenBase>().ToAllImplementations();
        builder.Bind<TimeService>().ToSelf().InSingletonScope();
    }

    protected override void Configure()
    {
        foreach(var dataService in Container.GetAll<ILocalStorageService>())
            dataService.Load();
        base.Configure();
    }

    protected override void OnUnhandledException(DispatcherUnhandledExceptionEventArgs e)
    {
        foreach(var dataService in Container.GetAll<ILocalStorageService>())
            dataService.Save();
        base.OnUnhandledException(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        foreach(var dataService in Container.GetAll<ILocalStorageService>())
            dataService.Save();
        base.OnExit(e);
    }
}