using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public class ColliderCollisionDetector : ControlledObject, ICollisionsProvider
    {
        public bool IsSingleCollisionMode = true;

        private bool _wasCollided;

        public event Action<CollisionPoint> OnCollision;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.layer != 8 )
            {
                if (collision.gameObject.layer != 9 )
                    if (collision.gameObject.layer != 10 )
                    {
                        {
                            Debug.Log("AnimacionEscudo");
                            if (!IsRunning)
                                return;

                            if (_wasCollided && IsSingleCollisionMode)
                                return;

                            if (!_wasCollided)
                                _wasCollided = true;

                            if (OnCollision != null)
                                OnCollision.Invoke(new CollisionPoint
                                {
                                    Point = collision.transform.position,
                                    Normal = (collision.transform.position - transform.position).normalized
                                });
                        }
                    }
            }
        }
    }
}
// collision.gameObject.layer != 13 && collision.gameObject.tag != "Pink" || (collision.gameObject.layer != 12 && collision.gameObject.tag != "Blue" )