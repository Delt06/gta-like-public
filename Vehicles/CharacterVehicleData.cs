using System;
using UnityEngine;

namespace Vehicles
{
    [Serializable]
    public struct CharacterVehicleData
    {
        [Min(0f)]
        public float EnterMaxDistance;
    }
}