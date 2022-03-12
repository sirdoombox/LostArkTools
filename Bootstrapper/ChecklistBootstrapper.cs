using System;
using System.Windows.Threading;
using FluentScheduler;
using LostArkChecklist.ViewModels;
using StyletIoC;

namespace LostArkChecklist.Bootstrapper;

public class ChecklistBootstrapper : Bootstrapper<ChecklistRootViewModel>
{
    protected override void Launch()
    {
        JobManager.UseUtcTime();
        JobManager.Initialize();
        base.Launch();
    }

    protected override void ConfigureIoC(IStyletIoCBuilder builder)
    {
        
    }
}