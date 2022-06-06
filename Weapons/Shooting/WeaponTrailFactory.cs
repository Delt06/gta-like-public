using UnityEngine;

namespace Weapons.Shooting
{
    public class WeaponTrailFactory
    {
        private readonly WeaponsStaticData _staticData;

        public WeaponTrailFactory(WeaponsStaticData staticData) => _staticData = staticData;

        public void Create(Ray ray, float distance)
        {
            var rotation = Quaternion.LookRotation(ray.direction, Vector3.up);
            var trail = Object.Instantiate(_staticData.WeaponTrailPrefab, ray.origin, rotation);
            var main = trail.main;
            var speed = main.startSpeed.constant;
            main.startLifetime = distance / speed;
        }
    }
}