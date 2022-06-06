using _Shared.States;

namespace Vehicles
{
    public struct InCarState : IState
    {
        public Car Car;
        public float Throttle;
        public float Steering;
        public bool IsBraking;
    }
}