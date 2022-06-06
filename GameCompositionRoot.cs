using _Shared;
using _Shared.UI;
using Ai;
using Characters;
using DELTation.DIFramework;
using DELTation.DIFramework.Containers;
using DELTation.LeoEcsExtensions.Composition.Di;
using Health;
using Sirenix.OdinInspector;
using UnityEngine;
using Weapons;
using Weapons.Shooting;

public class GameCompositionRoot : DependencyContainerBase
{
    [SerializeField] [Required] private SceneData _sceneData;
    [SerializeField] [Required] private StaticData _staticData;
    [SerializeField] [Required] private UiRoot _uiRoot;
    [SerializeField] [Required] private WeaponsStaticData _weaponsStaticData;
    [SerializeField] [Required] private WalkingZone _walkingZone;
    [SerializeField] [Required] private DamageTypesConfig _damageTypesConfig;
    [SerializeField] [Required] private CharacterFactoryConfig _characterFactoryConfig;

    protected override void ComposeDependencies(ICanRegisterContainerBuilder builder)
    {
        builder
            .RegisterEcsEntryPoint<GameEcsEntryPoint>()
            .AttachEcsEntryPointViewTo(gameObject)
            .Register<RuntimeData>()
            .Register(_sceneData)
            .Register(_staticData)
            .Register(_uiRoot)
            ;

        builder
            .Register(_weaponsStaticData).AsInternal()
            .Register<WeaponTrailFactory>()
            .Register(_damageTypesConfig).AsInternal()
            .Register<DamageTypesPriorityService>()
            .Register(_characterFactoryConfig)
            .Register<CharacterFactory>()
            ;

        builder
            .Register(_walkingZone)
            ;
    }
}