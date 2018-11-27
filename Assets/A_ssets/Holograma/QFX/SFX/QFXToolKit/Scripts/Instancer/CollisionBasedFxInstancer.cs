using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    [RequireComponent(typeof(ICollisionsProvider))]
    public class CollisionBasedFxInstancer : ControlledObject
    {
        public FxObject[] FxObjects;

        private bool _wasCollided;

        private void Awake()
        {
            var collisionProviders = GetComponents<ICollisionsProvider>();
            foreach (var collisionsProvider in collisionProviders)
            {
                collisionsProvider.OnCollision += delegate(CollisionPoint collisionPoint)
                {
                    if (_wasCollided)
                        return;

                    var targetRotation = Vector3.zero;

                    foreach (var fxObject in FxObjects)
                    {
                        switch (fxObject.FxRotation)
                        {
                            case FxRotationType.Default:
                                break;
                            case FxRotationType.Normal:
                                targetRotation = collisionPoint.Normal;
                                break;
                            case FxRotationType.LookAtEmitter:
                                targetRotation = GetComponent<IEmitterKeeper>().EmitterTransform.position;
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        FxObjectInstancer.InstantiateFx(fxObject, collisionPoint.Point, targetRotation);
                    }

                    _wasCollided = true;
                };
            }
        }
    }
}