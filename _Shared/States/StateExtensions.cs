using System;
using System.Runtime.CompilerServices;
using DELTation.LeoEcsExtensions.ExtendedPools;
using JetBrains.Annotations;
using Leopotam.EcsLite;

namespace _Shared.States
{
    public static class StateExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref TTo ChangeState<TFrom, TTo>(this EcsFilter filter, int entity)
            where TFrom : struct, IState where TTo : struct, IState
        {
            var world = filter.GetWorld();
            return ref ChangeState<TFrom, TTo>(world, entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref TTo ChangeState<TFrom, TTo>([NotNull] this EcsWorld world, int entity)
            where TFrom : struct, IState where TTo : struct, IState
        {
#if DEBUG
            if (world == null) throw new ArgumentNullException(nameof(world));
#endif

            var fromPool = world.GetObservablePool<TFrom>();
            var toPool = world.GetObservablePool<TTo>();
#if DEBUG
            if (!fromPool.Has(entity))
                throw new InvalidOperationException($"Entity is not in the state {typeof(TFrom).Name}.");
#endif
            fromPool.Del(entity);
            return ref toPool.Add(entity);
        }
    }
}