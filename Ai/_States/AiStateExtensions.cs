using System;
using System.Runtime.CompilerServices;
using DELTation.LeoEcsExtensions.ExtendedPools;
using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.EcsLite;

namespace Ai._States
{
    public static class AiStateExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref TTo ChangeAiState<TFrom, TTo>(this EcsFilter filter, int entity)
            where TFrom : struct, IAiState where TTo : struct, IAiState
        {
            var world = filter.GetWorld();
            return ref ChangeAiState<TFrom, TTo>(world, entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref TTo ChangeAiState<TFrom, TTo>(this EcsWorld world, int entity)
            where TFrom : struct, IAiState where TTo : struct, IAiState
        {
#if DEBUG
            if (world == null) throw new ArgumentNullException(nameof(world));
#endif

            var fromPool = world.GetObservablePool<TFrom>();
            var toPool = world.GetObservablePool<TTo>();
            var changeEventPool = world.GetPool<AiStateChangedEvent>();
#if DEBUG
            if (!fromPool.Has(entity))
                throw new InvalidOperationException($"Entity is not in the state {typeof(TFrom).Name}.");
#endif
            fromPool.Del(entity);
            changeEventPool.GetOrAdd(entity);
            return ref toPool.Add(entity);
        }
    }
}