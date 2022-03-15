using System.Windows;
using System.Windows.Threading;
using LostArkChecklist.Features.Root;
using LostArkChecklist.Features.Shared;
using LostArkChecklist.Services;
using StyletIoC;

namespace LostArkChecklist.Bootstrapper;

public class ChecklistBootstrapper : Bootstrapper<ApplicationRootViewModel>
{
    private readonly UserDataService _userDataService = new();
    private readonly TimeService _timeService = new();

    protected override void OnStart()
    {
        _userDataService.LoadUserData();
    }

    protected override void ConfigureIoC(IStyletIoCBuilder builder)
    {
        builder.Bind<UserDataService>().ToInstance(_userDataService);
        builder.Bind<TimeService>().ToInstance(_timeService);
        builder.Bind<FeatureScreenBase>().ToAllImplementations();
    }

    protected override void OnUnhandledException(DispatcherUnhandledExceptionEventArgs e)
    {
        _userDataService.SetLastOpened();
        _userDataService.SaveUserData();
        base.OnUnhandledException(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _userDataService.SetLastOpened();
        _userDataService.SaveUserData();
        base.OnExit(e);
    }
}