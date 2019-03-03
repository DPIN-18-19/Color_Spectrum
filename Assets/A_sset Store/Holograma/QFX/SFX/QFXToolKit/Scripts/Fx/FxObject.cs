using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace QFXToolKit
{
    [Serializable]
    public class FxObject : ICloneable
    {
        public GameObject Fx;
        public FxRotationType FxRotation = FxRotationType.Normal;

        public virtual object Clone()
        {
            return new FxObject
            {
                Fx = Fx,
                FxRotation = FxRotation
            };
        }
    }
}