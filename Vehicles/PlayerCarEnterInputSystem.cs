using _Shared;
using _Shared.States;
using DELTation.LeoEcsExtensions.ExtendedPools;
using Leopotam.EcsLite;
using UnityEngine;

namespace Vehicles
{
    public class PlayerCarEnterInputSystem : IEcsRunSystem
    {
        private readonly EcsReadWritePool<CarEnterCommand> _enterCommands;
        private readonly EcsReadWritePool<CarExitCommand> _exitCommands;
        private readonly EcsFilter _idleFilter;
        private readonly EcsFilter _inCarFilter;

        public PlayerCarEnterInputSystem(EcsWorld world)
        {
            _idleFilter = world.Filter<PlayerTag>().Inc<CharacterVehicleData>()
                .Inc<IdleState>().Exc<CarEnterCommand>()
                .End();
            _inCarFilter = world.Filter<PlayerTag>().Inc<CharacterVehicleData>()
                .Inc<InCarState>().Exc<CarExitCommand>()
                .End();
            _enterCommands = world.GetReadWritePool<CarEnterCommand>();
            _exitCommands = world.GetReadWritePool<CarExitCommand>();
        }

        public void Run(EcsSystems systems)
        {
            if (!Input.GetKeyDown(KeyCode.E)) return;

            foreach (var i in _idleFilter)
            {
                _enterCommands.Add(i);
            }

            foreach (var i in _inCarFilter)
            {
                _exitCommands.Add(i);
            }
        }
    }
}