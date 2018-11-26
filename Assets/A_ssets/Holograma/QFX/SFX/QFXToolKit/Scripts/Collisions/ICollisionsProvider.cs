using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    public interface ICollisionsProvider
    {
        event Action<CollisionPoint> OnCollision;
    }
}