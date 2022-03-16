using System.Linq;
using LostArkTools.Services;
using StyletIoC;

namespace LostArkTools.Extensions;

public static class StyletExtensions
{
    public static TImplementation GetSpecificImplementation<TImplementation, T>(this IContainer container) where TImplementation : T 
        => (TImplementation)container.GetAll<T>().FirstOrDefault(x => x.GetType().IsAssignableTo(typeof(TImplementation)));
}