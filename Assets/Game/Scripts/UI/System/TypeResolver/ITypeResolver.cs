using System;

namespace Game.UI.TypeResolver
{
    public interface ITypeResolver
    {
        Type Resolve(string presenterName);
    }
}