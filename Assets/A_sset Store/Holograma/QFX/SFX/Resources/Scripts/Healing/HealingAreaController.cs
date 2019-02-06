using System.Collections.Generic;
using QFXToolKit;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFX.SFX
{
    public class HealingAreaController : ControlledObject
    {
        public GameObject HealingAreaFx;
        public GameObject HealingFx;

        public float HealingRadius;
        public LayerMask LayerMask;

        private ParticleSystem _characterHealingPs;

        private readonly Dictionary<Healable, ParticleSystem>
            _healableObjectsInArea = new Dictionary<Healable, ParticleSystem>();

        public override void Run()
        {
            base.Run();

            var healingAreaGo = Instantiate(HealingAreaFx, transform.position, transform.rotation);
            healingAreaGo.transform.parent = transform;
        }

        private void FixedUpdate()
        {
            if (!IsRunning)
                return;

            var healableObjects =
                ObjectAreaFinder.FindObjects<Healable>(transform.position, HealingRadius * 1.5f, LayerMask);

            foreach (var healable in healableObjects)
            {
                var distance = Vector3.Distance(transform.position, healable.transform.position);

                if (_healableObjectsInArea.ContainsKey(healable))
                {
                    if (distance > HealingRadius)
                    {
                        var healingParticleSystem = _healableObjectsInArea[healable];
                        healingParticleSystem.Stop();
                        Destroy(healingParticleSystem.gameObject, 1f);
                        _healableObjectsInArea.Remove(healable);
                    }
                    continue;
                }

                if (distance > HealingRadius)
                    continue;

                var collider = healable.GetComponent<Collider>();
                var center = collider.bounds.center;

                var offset = new Vector3(0, center.y, 0);
                var targetPosition = healable.transform.position + offset;
                var healingGo = Instantiate(HealingFx, targetPosition, Quaternion.identity);
                healingGo.transform.parent = healable.transform;

                var healingPs = healingGo.GetComponent<ParticleSystem>();
                _healableObjectsInArea[healable] = healingPs;
            }
        }
    }
}