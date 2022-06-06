using DELTation.LeoEcsExtensions.Views;
using JetBrains.Annotations;

namespace _Shared
{
    public class RuntimeData
    {
        [CanBeNull]
        public IEntityView Player { get; set; }
    }
}