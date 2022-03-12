using System;
using System.Windows.Threading;
using LostArkChecklist.ViewModels;
using StyletIoC;

namespace LostArkChecklist.Bootstrapper;

public class ChecklistBootstrapper : Bootstrapper<ChecklistRootViewModel>
{
    protected override void ConfigureIoC(IStyletIoCBuilder builder)
    {
        var timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        timer.Start();
        builder.Bind<DispatcherTimer>().ToInstance(timer);
    }
}