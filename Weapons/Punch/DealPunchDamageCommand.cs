namespace Weapons.Punch
{
    public struct DealPunchDamageCommand
    {
        public Hand HandType;
    }

    public enum Hand
    {
        Left,
        Right,
    }
}