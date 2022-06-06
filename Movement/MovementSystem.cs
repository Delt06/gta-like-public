using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Systems.Run;
using Leopotam.EcsLite;
using UnityEngine;

namespace Movement
{
    public class MovementSystem : EcsSystemBase, IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            var filter = Filter<MovementData>().Inc<EffectiveMovementSpeed>().Exc<PoseLock>().End();
            var movementDatas = World.GetReadWritePool<MovementData>();
            var effectiveMovementSpeeds = World.GetReadOnlyPool<EffectiveMovementSpeed>();

            foreach (var i in filter)
            {
                ref var movementData = ref movementDatas.Get(i);
                var speed = effectiveMovementSpeeds.Read(i).Speed;
                var motion = movementData.Direction * speed * Time.deltaTime;
                movementData.LastSpeed = speed;
                movementData.CharacterController.Move(motion);
            }
        }
    }
}