using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;

namespace Movement
{
    public class PoseLockCharacterControllerSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<MovementData>().End();
            var locks = World.GetReadOnlyPool<PoseLock>();
            var movementDatas = World.GetReadOnlyPool<MovementData>();
            foreach (var i in filter)
            {
                var characterController = movementDatas.Read(i).CharacterController;
                var enabled = characterController.enabled;
                var shouldBeEnabled = !locks.Has(i);
                if (enabled != shouldBeEnabled)
                    characterController.enabled = shouldBeEnabled;
            }
        }
    }
}