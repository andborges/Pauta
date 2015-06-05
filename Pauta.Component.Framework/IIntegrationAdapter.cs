using System;
using System.Collections.Generic;
using MobileItFramework.ComponentModel;
using MobileItFramework.Security;

namespace Pauta.Component.Framework
{
    public interface IIntegrationAdapter : IComponentInfo
    {
        ICollection<Class> GetAllClasses(Token token, DateTime date);

        Class GetClass(Token token, string id);
        
        bool SaveClass(Token token, Class myclass);
    }
}
