using _Shared;
using _Shared.States;
using Ai;
using Ai._States;
using Ai.Attack;
using Ai.Idle;
using Ai.RunAway;
using Characters;
using DELTation.LeoEcsExtensions.Composition.Di;
using Health;
using Health.Ragdoll;
using Health.Ui;
using Movement;
using Movement.Jump;
using Movement.Sprint;
using Movement.Walk;
using Vehicles;
using Weapons;
using Weapons.Active;
using Weapons.Aiming;
using Weapons.Ik;
using Weapons.Punch;
using Weapons.Shooting;

public class GameEcsEntryPoint : EcsEntryPoint
{
    public override void PopulateSystems(EcsFeatureBuilder featureBuilder)
    {
        // Init
        featureBuilder
            .CreateAndAdd<PlayerSpawnSystem>()
            .CreateAndAdd<NpcSpawnSystem>()
            .CreateAndAdd<PlayerCamerasInitSystem>()
            ;

        // Prepare frame
        featureBuilder
            .CreateAndAdd<UpdateCurrentWeaponInfo>()
            ;

        // AI
        featureBuilder
            .CreateAndAdd<AgentStuckWorkaroundSystem>()
            .CreateAndAdd<AgentResetPathOnStateChangeSystem>()
            .CreateAndAdd<AgentRunAwayPathResetSystem>()
            .CreateAndAdd<AgentAttackPathResetSystem>()
            .CreateAndAdd<AgentStartFindingDestinationSystem>()
            .CreateAndAdd<AgentIdleDestinationSystem>()
            .CreateAndAdd<AgentRunAwayDestinationSystem>()
            .CreateAndAdd<AgentAttackDestinationSystem>()
            .CreateAndAdd<AgentSetDestinationSystem>()
            .OneFrame<FindDestinationCommand>()
            .CreateAndAdd<AgentSnapSystem>()
            .CreateAndAdd<AgentMovementDirectionSystem>()
            .CreateAndAdd<AgentIdleSprintSystem>()
            .CreateAndAdd<AgentRunAwaySprintSystem>()
            .CreateAndAdd<AgentAttackSprintSystem>()
            .CreateAndAdd<AgentAttackLookAtSystem>()
            .CreateAndAdd<AgentAttackSystem>()
            .CreateAndAdd<AgentUneqiupWeaponOnAttackStopSystem>()
            .CreateAndAdd<AgentAimTargetAggressorSystem>()
            .CreateAndAdd<AggressionMemorySpreadSystem>()
            .OneFrame<AiStateChangedEvent>()
            ;

        // Player Input
        featureBuilder
            .CreateAndAdd<CursorLockSystem>()
            .CreateAndAdd<PlayerMovementInputSystem>()
            .CreateAndAdd<PlayerSprintInputSystem>()
            .CreateAndAdd<PlayerJumpInputSystem>()
            .CreateAndAdd<PlayerFireInputSystem>()
            .CreateAndAdd<ToggleWeaponInputSystem>()
            .CreateAndAdd<PlayerCarInputSystem>()
            .CreateAndAdd<PlayerCarEnterInputSystem>()
            .CreateAndAdd<PlayerAimingInputSystem>()
            ;

        // Aiming
        featureBuilder
            .CreateAndAdd<StartAimingSystem>()
            .OneFrame<StartAimingCommand>()
            .CreateAndAdd<StopAimingSystem>()
            .OneFrame<StopAimingCommand>()
            .CreateAndAdd<AimingCameraSystem>()
            .CreateAndAdd<AimRigSystem>()
            ;

        // Movement
        featureBuilder
            .CreateAndAdd<InCarMovementPoseSystem>()
            .CreateAndAdd<DeathPoseSystem>()
            .CreateAndAdd<PoseLockCharacterControllerSystem>()
            .CreateAndAdd<WeaponSprintSystem>()
            .CreateAndAdd<SprintAnimationDataDefaultValueSystem>()
            .CreateAndAdd<SprintMovementSpeedSystem>()
            .CreateAndAdd<WalkMovementSpeedSystem>()
            .CreateAndAdd<NormalMovementSpeedSystem>()
            .CreateAndAdd<StopIfNotIdleSystem>()
            .CreateAndAdd<MovementSystem>()
            .CreateAndAdd<MovementGravitySystem>()
            .CreateAndAdd<JumpSystem>()
            .OneFrame<JumpCommand>()
            .CreateAndAdd<AimingTargetRotationOverrideSystem>()
            .CreateAndAdd<MovementRotationSystem>()
            .OneFrame<TargetRotationOverride>()
            .CreateAndAdd<MovementAnimationSystem>()
            .CreateAndAdd<SprintAnimationSystem>()
            ;

        // Weapons
        featureBuilder
            .CreateAndAdd<PunchStartSystem>()
            .CreateAndAdd<PunchFinishSystem>()
            .OneFrame<FinishedPunchEvent>()
            .CreateAndAdd<PunchAnimationSystem>()
            .CreateAndAdd<HideWeaponIfNotIdle>()
            .CreateAndAdd<HideWeaponSystem>()
            .OneFrame<HideWeaponCommand>()
            .CreateAndAdd<TakeWeaponSystem>()
            .OneFrame<TakeWeaponCommand>()
            .CreateAndAdd<ToggleWeaponRigSystem>()
            .CreateAndAdd<PunchHitSystem>()
            .OneFrame<DealPunchDamageCommand>()
            .CreateAndAdd<WeaponUseSystem>()
            .CreateAndAdd<SimpleDestructibleHitSystem>()
            .CreateAndAdd<WeaponHitSelfDamageSystem>()
            .CreateAndAdd<WeaponHitDealDamageSystem>()
            .CreateAndAdd<WeaponIkSystem>()
            .OneFrame<FireCommand>()
            ;

        // Health
        featureBuilder
            .CreateAndAdd<DealDamageSystem>()
            .OneFrame<DealDamageCommand>()
            .CreateAndAdd<HealthDeathSystemIdleState>()
            .CreateAndAdd<HealthDeathSystemPunchingState>()
            .CreateAndAdd<HealthDeathSystemInCarState>()
            .CreateAndAdd<HealthViewLookAtCameraSystem>()
            .CreateAndAdd<HealthViewUpdateSystem>()
            .CreateAndAdd<DeathAgentDisableSystem>()
            .CreateAndAdd<DeathAnimationSystem>()
            ;

        // Vehicles
        featureBuilder
            .CreateAndAdd<CarEnterSystem>()
            .OneFrame<CarEnterCommand>()
            .CreateAndAdd<CarExitSystem>()
            .OneFrame<CarExitCommand>()
            .CreateAndAdd<CarCameraSystem>()
            .CreateAndAdd<CarMovementSystem>()
            .CreateAndAdd<InCarAnimationSystem>()
            ;

        // Camera
        featureBuilder
            .CreateAndAdd<CinemachineManualUpdateSystem>()
            .CreateAndAdd<PlayerAimTargetCameraSystem>()
            ;

        // AI Feedback
        featureBuilder
            .CreateAndAdd<AgentEffectiveMovementSpeedSystem>()
            .CreateAndAdd<AgentRunAwayForgetSystem>()
            .CreateAndAdd<AgentAttackForgetSystem>()
            .CreateAndAdd<WeaponHitAggressionSystem>()
            .CreateAndAdd<AgentRunAwayStartSystem>()
            .CreateAndAdd<AgentAttackStartSystem>()
            .CreateAndAdd<AgentEquipWeaponOnAggressionWithWeaponSystem>()
            .CreateAndAdd<AgentAimToggleSystem>()
            ;

        // UI Presentations
        featureBuilder
            .CreateAndAdd<PlayerHealthPresenterSystem>()
            ;

        // Cleanup
        featureBuilder
            .OneFrame<PoseLock>()
            .OneFrame<WeaponHitEvent>()
            .OneFrame<EffectiveMovementSpeed>()
            .OneFrame<CurrentWeaponInfo>()
            .OneFrameUpdateEvents()
            ;
    }
}