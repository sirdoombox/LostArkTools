using System.Windows;
using FluentScheduler;
using LostArkChecklist.Features.Root;
using LostArkChecklist.Services;
using StyletIoC;

namespace LostArkChecklist.Bootstrapper;

public class ChecklistBootstrapper : Bootstrapper<ApplicationRootViewModel>
{
    private readonly UserDataService _userDataService = new();

    protected override void OnStart()
    {
        JobManager.UseUtcTime();
        JobManager.Initialize();
        _userDataService.LoadUserData();
    }

    protected override void ConfigureIoC(IStyletIoCBuilder builder)
    {
        builder.Bind<UserDataService>().ToInstance(_userDataService);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _userDataService.SetLastOpened();
        _userDataService.SaveUserData();
        base.OnExit(e);
    }
}