using System;
using Sirenix.OdinInspector;

namespace _Shared.Attributes
{
    [IncludeMyAttributes]
    [AssetSelector(Filter = "t:prefab")]
    [Required]
    public class PrefabAssetSelectorAttribute : Attribute { }
}