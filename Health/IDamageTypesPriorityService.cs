namespace Health
{
    public interface IDamageTypesPriorityService
    {
        int GetPriority(DamageType damageType);
        DamageType ChooseWithHigherPriority(DamageType damageType1, DamageType damageType2);
    }
}