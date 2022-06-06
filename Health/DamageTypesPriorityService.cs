namespace Health
{
    public class DamageTypesPriorityService : IDamageTypesPriorityService
    {
        private readonly DamageTypesConfig _damageTypesConfig;

        public DamageTypesPriorityService(DamageTypesConfig damageTypesConfig) =>
            _damageTypesConfig = damageTypesConfig;

        public int GetPriority(DamageType damageType)
        {
            foreach (var damageTypePriority in _damageTypesConfig.DamageTypePriorities)
            {
                if (damageTypePriority.DamageType == damageType)
                    return damageTypePriority.Priority;
            }

            return _damageTypesConfig.FallbackPriority;
        }

        public DamageType ChooseWithHigherPriority(DamageType damageType1, DamageType damageType2)
        {
            var priority1 = GetPriority(damageType1);
            var priority2 = GetPriority(damageType2);
            return priority1 >= priority2 ? damageType1 : damageType2;
        }
    }
}