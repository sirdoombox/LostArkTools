using System.Linq;
using LostArkTools.Services;
using LostArkTools.Services.Base;
using StyletIoC;

namespace LostArkTools.Extensions;

public static class StyletExtensions
{
    public static TImplementation GetSpecificImplementation<TImplementation, T>(this IContainer container) 
        where TImplementation : T => 
        (TImplementation)container.GetAll<T>().FirstOrDefault(x => x.GetType().IsAssignableTo(typeof(TImplementation)));

    public static TStorageService GetStorageService<TStorageService>(this IContainer container)
        where TStorageService : ILocalStorageService =>
        container.GetSpecificImplementation<TStorageService, ILocalStorageService>();

}