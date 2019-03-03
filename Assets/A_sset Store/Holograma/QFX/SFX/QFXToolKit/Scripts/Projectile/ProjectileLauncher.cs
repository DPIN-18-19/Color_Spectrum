using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class ProjectileLauncher : ControlledObject
    {
        public GameObject Projectile;

        public override void Run()
        {
            base.Run();

            var projectile = Instantiate(Projectile, transform.position, transform.rotation)
                .GetComponent<ControlledObject>();

            var emitterKeeper = projectile.GetComponent<IEmitterKeeper>();
            if (emitterKeeper != null)
                emitterKeeper.EmitterTransform = transform;

            projectile.Run();
        }
    }
}