using System.Collections;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public sealed class SimpleProjectileWeapon : ControlledObject
    {
        public GameObject LaunchParticleSystem;

        public Transform LaunchTransform;

        public LightAnimator LightAnimator;

        public GameObject Projectile;

        public float FireRate = 0.5f;

        private bool _isFireAllowed = true;

        private ParticleSystem _launchPs;

        public override void Setup()
        {
            base.Setup();

            var launchGo = Instantiate(LaunchParticleSystem, transform, true);
            launchGo.transform.rotation = LaunchTransform.rotation;
            _launchPs = launchGo.GetComponent<ParticleSystem>();
        }

        public override void Run()
        {
            base.Run();

            LightAnimator.Run();

            _launchPs.transform.position = LaunchTransform.position;
        }

        public override void Stop()
        {
            base.Stop();

            LightAnimator.Stop();
        }

        private void Update()
        {
            if (!IsRunning)
                return;

            if (_isFireAllowed)
                StartCoroutine("Fire");
        }

        private IEnumerator Fire()
        {
            _isFireAllowed = false;

            _launchPs.Play(true);

            Vector3 position;
            Quaternion rotation;

            if (LaunchTransform != null)
            {
                position = LaunchTransform.position;
                rotation = LaunchTransform.rotation;
            }
            else
            {
                position = transform.position;
                rotation = transform.rotation;
            }

            var go = Instantiate(Projectile, position, rotation);
            var emitterKeeper = go.GetComponent<IEmitterKeeper>();
            if (emitterKeeper != null)
                emitterKeeper.EmitterTransform = transform;

            LightAnimator.Run();

            yield return new WaitForSeconds(FireRate);

            _isFireAllowed = true;
        }
    }
}